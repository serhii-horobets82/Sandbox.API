namespace Evoflare.API.Commands
{
    using Evoflare.API.ViewModels;
    using Boxed.AspNetCore;
    using Microsoft.AspNetCore.JsonPatch;

    public interface IPatchCarCommand : IAsyncCommand<int, JsonPatchDocument<SaveCar>>
    {
    }
}
