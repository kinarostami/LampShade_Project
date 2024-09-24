﻿using _0_Framework.Domain;
using AccountManagement.Domain.RoleAgg;

namespace AccountManagement.Domain.AccountAgg
{
    public class Account : EntityBase
    {
        public string Fullname { get; private set; }
        public string Username { get; private set; }
        public string Password { get; private set; }
        public string Mobile { get; private set; }
        public string ProfilePhoto { get; private set; }
        public long RoleId { get; private set; }
        public Role Roles { get; private set; }

        public Account(string fullname,string username,string password,string mobile,string profilePhoto,long roleId)
        {
            Fullname = fullname;
            Username = username;
            Password = password;
            Mobile = mobile;
            ProfilePhoto =profilePhoto;

            if (profilePhoto == null)
            {
                ProfilePhoto = $"businessicon.png";
            }

            RoleId = roleId;

            if (roleId == 0)
            {
                RoleId = 4;
            }

        }

        public void Edit(string fullname,string username,string mobile,string profilePhoto,long roleId)
        {
            Fullname = fullname;
            Username = username;
            Mobile = mobile;
            if (!string.IsNullOrWhiteSpace(profilePhoto))
            {
                ProfilePhoto = profilePhoto;
            }
            RoleId = roleId;
        }

        public void ChangePassword(string password)
        {
            Password = password;
        }
    }
}
