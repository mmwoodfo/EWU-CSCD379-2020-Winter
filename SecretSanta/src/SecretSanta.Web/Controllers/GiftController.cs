using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SecretSanta.Web.Api;

namespace SecretSanta.Web.Controllers
{
    public class GiftController : Controller
    {
        public IHttpClientFactory ClientFactory { get; }

        public GiftController(IHttpClientFactory clientFactory)
        {
            if (clientFactory is null)
                throw new System.ArgumentNullException(nameof(clientFactory));

            ClientFactory = clientFactory;
        }

        // GET: Gift
        public async Task<ActionResult> IndexAsync()
        {
            HttpClient httpClient = ClientFactory.CreateClient("SantaApi");
            var client = new GiftClient(httpClient);

            ICollection<Gift> users = await client.GetAllAsync();

            return View(users);
        }
    }
}