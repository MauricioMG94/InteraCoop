using InteraCoop.Backend.Data;
using InteraCoop.Backend.Helpers;
using InteraCoop.Backend.Repositories.Interfaces;
using InteraCoop.Backend.UnitsOfWork.Implementations;
using InteraCoop.Shared.Dtos;
using InteraCoop.Shared.Entities;
using InteraCoop.Shared.Responses;
using Microsoft.EntityFrameworkCore;

namespace InteraCoop.Backend.Repositories.Implementations
{
    public class CampaignsRepository : GenericRepository<Campaign>, ICampaignsRepository
    {
        private readonly DataContext _context;

        public CampaignsRepository(DataContext context) : base(context)
        {
            _context = context;
        }

        public override async Task<ActionResponse<Campaign>> GetAsync(int id)
        {
            var campaign = await _context.Campaigns
                .Include(x => x.ProductsList!)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (campaign == null)
            {
                return new ActionResponse<Campaign>
                {
                    WasSuccess = false,
                    Message = "Campaña no existe"
                };
            }
            return new ActionResponse<Campaign>
            {
                WasSuccess = true,
                Result = campaign
            };
            
        }

        public override async Task<ActionResponse<IEnumerable<Campaign>>> GetAsync(PaginationDTO pagination)
        {
            var queryable = _context.Campaigns
            .Include(x => x.ProductsList)
            .AsQueryable();

            if (!string.IsNullOrWhiteSpace(pagination.Filter))
            {
                queryable = queryable.Where(x => x.CampaignName.ToLower().Contains(pagination.Filter.ToLower()));
            }

            return new ActionResponse<IEnumerable<Campaign>>
            {
                WasSuccess = true,
                Result = await queryable
                    .OrderBy(x => x.StartDate)
                    .Paginate(pagination)
                    .ToListAsync()
            };
        }

        public override async Task<ActionResponse<int>> GetTotalPagesAsync(PaginationDTO pagination)
        {
            var queryable = _context.Campaigns.AsQueryable();

            if (!string.IsNullOrWhiteSpace(pagination.Filter))
            {
                queryable = queryable.Where(x => x.CampaignName.ToLower().Contains(pagination.Filter.ToLower()));
            }

            double count = await queryable.CountAsync();
            int totalPages = (int)Math.Ceiling(count / pagination.RecordsNumber);
            return new ActionResponse<int>
            {
                WasSuccess = true,
                Result = totalPages
            };
        }

        public async Task<ActionResponse<Campaign>> AddAsync(CampaignDto campaignDto)
        {
            try
            {
                var newCampaign = new Campaign
                {
                    CampaignId = campaignDto.CampaignId,
                    CampaignName = campaignDto.CampaignName, 
                    CampaignType = campaignDto.CampaignType,
                    Status = campaignDto.Status,
                    Description = campaignDto.Description,
                    StartDate = campaignDto.StartDate,
                    EndDate = campaignDto.EndDate,
                    ProductsList = new List<Product>(),
                };
                foreach (var productId in campaignDto.ProductsIds!)
                {
                    var product = await _context.Products.FirstOrDefaultAsync(x => x.Id == productId);
                    if (product != null)
                    {
                        newCampaign.ProductsList.Add(product);
                    }
                }
                _context.Add(newCampaign);
                await _context.SaveChangesAsync();
                return new ActionResponse<Campaign>
                    {
                        WasSuccess = true,
                        Result = newCampaign
                    };
                }
                catch (DbUpdateException)
                {
                    return new ActionResponse<Campaign>
                    {
                        WasSuccess = false,
                        Message = "Ya existe una campaña con el mismo nombre."
                    };
            }
            catch (Exception exception)
            {
                return new ActionResponse<Campaign>
                {
                    WasSuccess = false,
                    Message = exception.Message
                };
            }
        }

        public async Task<ActionResponse<Campaign>> UpdateAsync(CampaignDto campaignDto)
        {
            try
            {
                var campaign = await _context.Campaigns
                    .Include(x => x.ProductsList)
                    .FirstOrDefaultAsync(x => x.Id == campaignDto.Id);
                if (campaign == null)
                {
                    return new ActionResponse<Campaign>
                    {
                        WasSuccess = false,
                        Message = "Campaña no existe"
                    };
                }

                campaign.CampaignId = campaignDto.CampaignId;
                campaign.CampaignName = campaignDto.CampaignName;
                campaign.CampaignType = campaignDto.CampaignType;
                campaign.Status = campaignDto.Status;
                campaign.StartDate = campaignDto.StartDate;
                campaign.EndDate = campaignDto.EndDate;
                campaign.ProductsList = new List<Product>();

                if (campaignDto.ProductsIds != null)
                {
                    foreach (var productId in campaignDto.ProductsIds)
                    {
                        var product = await _context.Products.FindAsync(productId);
                        if (product != null)
                        {
                            campaign.ProductsList.Add(product);
                        }
                    }
                }

                _context.Update(campaign);
                await _context.SaveChangesAsync();
                return new ActionResponse<Campaign>
                {
                    WasSuccess = true,
                    Result = campaign
                };
            }
            catch (DbUpdateException)
            {
                return new ActionResponse<Campaign>
                {
                    WasSuccess = false,
                    Message = "Ya existe una Campaña con el mismo identificador."
                };
            }
            catch (Exception exception)
            {
                return new ActionResponse<Campaign>
                {
                    WasSuccess = false,
                    Message = exception.Message
                };
            }
        }

        public override async Task<ActionResponse<Campaign>> DeleteAsync(int id)
        {
            var campaign = await _context.Campaigns
            .Include(x => x.ProductsList)
            .FirstOrDefaultAsync(x => x.Id == id);
            if (campaign == null)
            {
                return new ActionResponse<Campaign>
                {
                    WasSuccess = false,
                    Message = "Campaña no encontrada"
                };
            }

            try
            {
                _context.Products.RemoveRange(campaign.ProductsList!);
                _context.Campaigns.Remove(campaign);
                await _context.SaveChangesAsync();
                return new ActionResponse<Campaign>
                {
                    WasSuccess = true,
                };
            }
            catch
            {
                return new ActionResponse<Campaign>
                {
                    WasSuccess = false,
                    Message = "No se puede borrar la campaña, porque tiene registros relacionados"
                };
            }
        }

    }
}
