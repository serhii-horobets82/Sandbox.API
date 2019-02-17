namespace Evoflare.API.Commands
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using Evoflare.API.Repositories;
    using Evoflare.API.ViewModels;
    using Boxed.Mapping;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Infrastructure;
    using Microsoft.Extensions.Primitives;
    using Microsoft.Net.Http.Headers;

    public class GetCarCommand : IGetCarCommand
    {
        private readonly IActionContextAccessor actionContextAccessor;
        private readonly ICarRepository carRepository;
        private readonly IMapper<Models.Car, Car> carMapper;

        public GetCarCommand(
            IActionContextAccessor actionContextAccessor,
            ICarRepository carRepository,
            IMapper<Models.Car, Car> carMapper)
        {
            this.actionContextAccessor = actionContextAccessor;
            this.carRepository = carRepository;
            this.carMapper = carMapper;
        }

        public async Task<IActionResult> ExecuteAsync(int carId, CancellationToken cancellationToken)
        {
            var car = await this.carRepository.Get(carId, cancellationToken);
            if (car == null)
            {
                return new NotFoundResult();
            }

            var httpContext = this.actionContextAccessor.ActionContext.HttpContext;
            if (httpContext.Request.Headers.TryGetValue(HeaderNames.IfModifiedSince, out var stringValues))
            {
                if (DateTimeOffset.TryParse(stringValues, out var modifiedSince) &&
                    (modifiedSince >= car.Modified))
                {
                    return new StatusCodeResult(StatusCodes.Status304NotModified);
                }
            }

            var carViewModel = this.carMapper.Map(car);
            httpContext.Response.Headers.Add(HeaderNames.LastModified, car.Modified.ToString("R"));
            return new OkObjectResult(carViewModel);
        }
    }
}
