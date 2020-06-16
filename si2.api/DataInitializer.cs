﻿using Microsoft.AspNetCore.Identity;
using si2.dal.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace si2.api
{
    public class DataSeeder : IDataSeeder
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        private readonly string SUPER_ADMIN_EMAIL = "superadmin@si2.com";
        private readonly string SUPER_ADMIN_PASSD = "Super_123";
        private readonly string SUPER_ADMIN_FIRSTNAMEAR = "سوبر";
        private readonly string SUPER_ADMIN_LASTNAMEAR = "أدمن";
        private readonly string SUPER_ADMIN_FIRSTNAMEFR = "Super";
        private readonly string SUPER_ADMIN_LASTNAMEFR = "Admin";

        private readonly string[] ROLES = new string[3] { "SuperAdmin", "Administrator", "User" };

        public DataSeeder(
                UserManager<ApplicationUser> userManager,
                RoleManager<IdentityRole> roleManager  )
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public IDataSeeder SeedUsers ()
        {
            if (_userManager.FindByEmailAsync("superadmin@si2.com").Result == null)
            {
                ApplicationUser user = new ApplicationUser()
                {
                    Email = SUPER_ADMIN_EMAIL,
                    UserName = SUPER_ADMIN_EMAIL,
                    EmailConfirmed = false,
                    FirstNameAr = SUPER_ADMIN_FIRSTNAMEAR,
                    LastNameAr = SUPER_ADMIN_LASTNAMEAR,
                    FirstNameFr = SUPER_ADMIN_FIRSTNAMEFR,
                    LastNameFr = SUPER_ADMIN_LASTNAMEFR
                };

                IdentityResult result = _userManager.CreateAsync(user, SUPER_ADMIN_PASSD).Result;

                if (result.Succeeded)
                {
                    _userManager.AddToRoleAsync(user,"SuperAdmin").Wait();
                }
            }
            return this;
        }

        public IDataSeeder SeedRoles()
        {
            foreach (var role in ROLES)
            {
                if (!_roleManager.RoleExistsAsync(role).Result)
                {
                    IdentityRole identityRole = new IdentityRole();
                    identityRole.Name = role;
                    IdentityResult roleResult = _roleManager.CreateAsync(identityRole).Result;
                }
            }
            return this;
        }
    }

    public interface IDataSeeder
    {
        IDataSeeder SeedRoles();
        IDataSeeder SeedUsers();
    }
}
