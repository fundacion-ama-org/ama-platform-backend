using System.ComponentModel.DataAnnotations;

namespace PlatformAMA.Modules.Brigadas.Models;

public class Brigada {
	[Key] 
	public int Id { get; set; }

		[Required] public string Nombre_Brigada { get; set; }

		public string Descripcion_Brigada { get; set; }

		public string Nombre_Responsable { get; set; }

		public int Num_Responsable { get; set; }

		public string Direccion { get; set; }

		public DateTime Inicio_Brigada { get; set; }

		public DateTime? Fin_Brigada { get; set; }

	public DateTime created_at { get; set; }

	public DateTime updated_at { get; set; }
}