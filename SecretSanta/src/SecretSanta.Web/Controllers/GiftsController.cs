﻿using Microsoft.AspNetCore.Mvc;
using SecretSanta.Web.Api;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace SecretSanta.Web.Controllers
{
    public class GiftsController : Controller
    {
        public IHttpClientFactory ClientFactory { get; }
        private GiftClient Client { get; }

        public GiftsController(IHttpClientFactory clientFactory)
        {
            HttpClient httpClient = clientFactory?.CreateClient("SecretSantaApi") ?? throw new ArgumentNullException(nameof(clientFactory));
            Client = new GiftClient(httpClient);

            ClientFactory = clientFactory;
        }

        public async Task<IActionResult> Index()
        {
            ICollection<Gift> gifts = await Client.GetAllAsync();
            return View(gifts);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Create(GiftInput giftInput)
        {
            ActionResult result = View(giftInput);

            if (ModelState.IsValid)
            {
                await Client.PostAsync(giftInput);

                result = RedirectToAction(nameof(Index));
            }

            return result;
        }

        public async Task<ActionResult> Edit(int id)
        {
            ActionResult result = View(id);

            if (ModelState.IsValid)
            {
                var fetchedGift = await Client.GetAsync(id);
                result = View(fetchedGift);
            }

            return result;
        }

        [HttpPost]
        public async Task<ActionResult> Edit(int id, GiftInput giftInput)
        {
            ActionResult result = View();

            if (ModelState.IsValid)
            {
                await Client.PutAsync(id, giftInput);

                result =  RedirectToAction(nameof(Index));
            }

            return result;
        }

        public async Task<ActionResult> Delete(int id)
        {
            await Client.DeleteAsync(id);

            return RedirectToAction(nameof(Index));
        }
    }
}