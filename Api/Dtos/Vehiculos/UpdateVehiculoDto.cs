using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Dtos.Vehiculos;
public record UpdateVehiculoDto( int Año, string NumeroSerie, int Kilometraje);