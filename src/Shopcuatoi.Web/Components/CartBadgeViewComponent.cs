﻿using System;
using System.Security.Claims;
using Microsoft.AspNet.Mvc;
using Shopcuatoi.Orders.ApplicationServices;
using Shopcuatoi.Web.Extensions;

namespace Shopcuatoi.Web.Components
{
    public class CartBadgeViewComponent : ViewComponent
    {
        private readonly ICartService cartService;

        public CartBadgeViewComponent(ICartService cartService)
        {
            this.cartService = cartService;
        }

        public IViewComponentResult Invoke()
        {
            long? userId = null;
            Guid? guestId = null;

            if (HttpContext.User.IsSignedIn())
            {
                userId = long.Parse(HttpContext.User.GetUserId());
            }
            else
            {
                guestId = GuestIdentityManager.GetGuestId(HttpContext);
            }

            var cartItemCount = cartService.GetCartItems(userId, guestId).Count;

            return View(cartItemCount);
        }
    }
}