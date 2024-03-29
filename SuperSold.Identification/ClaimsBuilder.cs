﻿using System.ComponentModel.DataAnnotations;
using System.Security.Claims;

namespace SuperSold.Identification;

public class ClaimsBuilder {

    private readonly ClaimsIdentity _identity;

    public ClaimsBuilder(string authenticationType) {
        _identity = new(new List<Claim>(), authenticationType);
    }

    public ClaimsBuilder AddClaim(Claim claim) {
        _identity.AddClaim(claim);
        return this;
    }

    public ClaimsBuilder AddClaimRange(IEnumerable<Claim> claims) {
        foreach(var claim in claims) {
            AddClaim(claim);
        }
        return this;
    }

    public ClaimsPrincipal BuildPrincipal() {
        return new ClaimsPrincipal(_identity);
    }

}