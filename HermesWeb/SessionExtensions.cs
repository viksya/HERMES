using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HermesWeb
{
    public static class SessionExtensions
    {
        public const string SESSION_USERNAME = "username";
       
        public static void SetUsername(this ISession session, string username)
        {
            session.SetString(SESSION_USERNAME, username);

        }

        public static string GetUsername(this ISession session)
        {
            return session.GetString(SESSION_USERNAME);
        }

        public static bool isSignedIn(this ISession session)
        {
            return session.Keys.Contains(SESSION_USERNAME);
        }

       
    }
}

