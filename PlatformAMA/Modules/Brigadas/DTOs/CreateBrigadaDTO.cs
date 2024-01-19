namespace PlatformAMA.Modules.Brigadas.DTOs;

public class CreateBrigadaDTO {
	public string Nombre_Brigada { get; set; }

	public string Descripcion_Brigada { get; set; }

	public string Nombre_Responsable { get; set; }

	public int Num_Responsable { get; set; }

	public string Direccion { get; set; }

	public DateTime Inicio_Brigada { get; set; }

	public DateTime? Fin_Brigada { get; set; }
}