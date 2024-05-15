using Microsoft.AspNetCore.Authorization;

namespace InteraCoop.Frontend.Pages.Campaigns
{
    [Authorize(Roles = "Analist")]
    public partial class CampaignDetails
    {

    }
}