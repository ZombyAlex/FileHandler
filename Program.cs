
namespace FileHandler
{
	class Program
	{
		static void Main(string[] args)
		{
			FileHandler fileHandler = new FileHandler(args);
			fileHandler.Execute();
		}
	}
}
