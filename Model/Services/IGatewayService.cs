using System.Threading.Tasks;

namespace SharedComponent.Services
{
	public interface IGatewayService
	{
		Task<bool> Post(decimal amount);
	}
}
