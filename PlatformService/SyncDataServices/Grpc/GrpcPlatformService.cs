using System.Threading.Tasks;
using AutoMapper;
using Grpc.Core;
using PlatformService.Data;

namespace PlatformService.SyncDataServices.Grpc
{
    public class GrpcPlatformService : GrpcPlatform.GrpcPlatformBase
    {
        private readonly IPlatformRepo _repository;
        private readonly IMapper _mapper;
        public GrpcPlatformService(IPlatformRepo repository, IMapper mapper)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public override Task<PlatformResponse> GetAllPlatforms(GetAllRequest request,
             ServerCallContext context)
        {
            var response = new PlatformResponse();
            var platforms = _repository.GetAllPlatforms();

            foreach (var platform in platforms)
            {
                response.Platform.Add(_mapper.Map<GrpcPlatformModel>(platform));           
            }

            return Task.FromResult(response);
        }
    }
}