using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SharedComponent.Services
{
	public  interface IGatewayService
	{
		Task<bool> Post(decimal amount);
	}
}
