﻿
using System;
using System.Collections.Generic;
using System.Linq;
using Nancy;
using Nancy.Authentication.Forms;
using Nancy.Security;

namespace Photon.Web.Security
{
	public class PhotonUserMapper:IUserMapper
	{
		private static List<Tuple<string, string, Guid>> users = new List<Tuple<string, string, Guid>>();

        static PhotonUserMapper()
        {
            users.Add(new Tuple<string, string, Guid>("admin", "password", new Guid("55E1E49E-B7E8-4EEA-8459-7A906AC4D4C0")));
            users.Add(new Tuple<string, string, Guid>("user", "password", new Guid("56E1E49E-B7E8-4EEA-8459-7A906AC4D4C0")));
        }

        public IUserIdentity GetUserFromIdentifier(Guid identifier, NancyContext context)
        {
            var userRecord = users.Where(u => u.Item3 == identifier).FirstOrDefault();

            return userRecord == null
                       ? null
                       : new PhotonUserIdentity {UserName = userRecord.Item1};
        }

        public static Guid? ValidateUser(string username, string password)
        {
            var userRecord = users.Where(u => u.Item1 == username && u.Item2 == password).FirstOrDefault();

            if (userRecord == null)
            {
                return null;
            }

            return userRecord.Item3;
        }
	}
}