using Microsoft.AspNetCore.Authorization;

namespace InteraCoop.Frontend.Pages.Campaigns
{
    [Authorize(Roles = "Admin")]
    public partial class CampaignDetails
    {

    }
}