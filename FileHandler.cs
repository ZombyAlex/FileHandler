using System.Collections.Generic;

namespace FileHandler
{
	class FileHandler
	{
		List<FileHandlerCommand> fileHandlerCommands = new List<FileHandlerCommand>();

		public FileHandler(string[] args)
		{
			string filename = string.Empty;
			string command = string.Empty;
			string str = string.Empty;
			for (int i = 0; i < args.Length; i++)
			{
				if (args[i] == "-h")
				{
					fileHandlerCommands.Add(new FileHandlerCommandHelp());
				}
				else if (args[i] == "-f")
				{
					if (args.Length > i + 1)
					{
						filename = args[i + 1];
						i++;
					}
				}
				else if (args[i] == "-m")
				{
					if (args.Length > i + 1)
					{
						command = args[i + 1];
						i++;
					}
				}
				else if (args[i] == "-s")
				{
					if (args.Length > i + 1)
					{
						str = args[i + 1];
						i++;
					}
				}
			}

			if (command == "find")
			{
				fileHandlerCommands.Add(new FileHandlerCommandFind(filename, str));
			}
			else if (command == "checksum")
			{
				fileHandlerCommands.Add(new FileHandlerCommandChecksum(filename));
			}
		}

		public void Execute()
		{
			foreach (var fileHandlerCommand in fileHandlerCommands)
			{
				fileHandlerCommand.Execute();
			}
		}
	}
}
