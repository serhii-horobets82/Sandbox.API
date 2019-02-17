namespace Evoflare.API.Commands
{
    using Evoflare.API.ViewModels;
    using Boxed.AspNetCore;

    public interface IGetCarPageCommand : IAsyncCommand<PageOptions>
    {
    }
}
