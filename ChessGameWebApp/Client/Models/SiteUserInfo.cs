﻿using System.Security.Claims;

namespace ChessGameWebApp.Client
{
    public class SiteUserInfo
    {
        public string Name { get; set; }
        public int Id { get; set; }
        public List<string> Roles { get; set; }
        public DateTime AccessTokenExpire { get; set; }

        public void Update(IEnumerable<Claim> claims)
        {
            Name = claims.First(c => c.Type.Equals("email")).Value;
            Id = int.Parse(claims.First(c => c.Type.Equals("nameid")).Value);
            Roles = claims
                .Where(claim => claim.Type.Equals("role"))
                .Select(c => c.Value)
                .ToList();
        }
    }
}