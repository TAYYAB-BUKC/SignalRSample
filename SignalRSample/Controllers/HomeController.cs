using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using SignalRSample.Hubs;
using SignalRSample.Models;
using System.Diagnostics;

namespace SignalRSample.Controllers
{
	public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IHubContext<VotingHub> _votingHubContext; 

		public HomeController(ILogger<HomeController> logger, IHubContext<VotingHub> votingHubContext)
        {
            _logger = logger;
            _votingHubContext = votingHubContext;
		}

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

		public async Task<IActionResult> AddVote(string voterID)
		{
            if (StaticDetails.StaticDetails.VoteCounts.ContainsKey(voterID))
            {
                StaticDetails.StaticDetails.VoteCounts[voterID]++;
			}

            await _votingHubContext.Clients.All.SendAsync("UpdateVotingStatus",
                StaticDetails.StaticDetails.VoteCounts[StaticDetails.StaticDetails.VoterA],
                StaticDetails.StaticDetails.VoteCounts[StaticDetails.StaticDetails.VoterB],
                StaticDetails.StaticDetails.VoteCounts[StaticDetails.StaticDetails.VoterC]
            );
			return Accepted();
		}

		public IActionResult Notification()
		{
			return View();
		}

		public IActionResult Voting()
		{
			return View();
		}

		public IActionResult House()
		{
			return View();
		}

		public IActionResult BasicChatApp()
		{
			return View();
		}
	}
}
