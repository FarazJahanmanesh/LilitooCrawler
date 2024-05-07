using Domain.Interfaces.ExternalServices;
using Domain.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services;
public class LilitooReadServices: ILilitooReadServices
{
	private readonly ILilitooServices _lilitooServices;
    public LilitooReadServices(ILilitooServices lilitooServices)
	{
        _lilitooServices = lilitooServices;
    }
}
