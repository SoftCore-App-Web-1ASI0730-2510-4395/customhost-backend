using customhost_backend.IAM.Domain.Model.Aggregates;
using customhost_backend.IAM.Interfaces.REST.Resources;

namespace customhost_backend.IAM.Interfaces.REST.Transform;

public static class AuthenticatedUserResourceFromEntityAssembler
{
    public static AuthenticatedUserResource ToResourceFromEntity(
        User user, string token)
    {
        return new AuthenticatedUserResource(user.Id, user.Username, token);
    }
}