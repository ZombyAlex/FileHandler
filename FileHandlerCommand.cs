using System;
using System.IO;

namespace FileHandler
{
	class FileHandlerCommand
	{
		public virtual void Execute()
		{
		}
	}

	class FileHandlerCommandFind : FileHandlerCommand
	{
		private readonly string filename;
		private readonly string findText;

		public FileHandlerCommandFind(string filename, string findText)
		{
			this.filename = filename;
			this.findText = findText;
		}

		public override void Execute()
		{
			if (File.Exists(filename))
			{
				StreamReader streamReader = new StreamReader(filename);

				string text = streamReader.ReadToEnd();

				int offset = 0;
				while (true)
				{
					offset = text.IndexOf(findText, offset, StringComparison.Ordinal);
					if (offset == -1)
						break;
					Console.WriteLine("offset string {0} = {1}", findText, offset);
					offset++;
				}

			}
		}
	}

	class FileHandlerCommandChecksum : FileHandlerCommand
	{
		private readonly string filename;

		public FileHandlerCommandChecksum(string filename)
		{
			this.filename = filename;
		}

		public override void Execute()
		{
			if (File.Exists(filename))
			{
				FileStream fileStream = File.Open(filename, FileMode.Open);
				using (BinaryReader reader = new BinaryReader(fileStream))
				{
					byte[] bytes = reader.ReadBytes((int)fileStream.Length);
					if (bytes.Length % 4 != 0)
					{
						int len = (bytes.Length / 4 + 1) * 4;
						byte[] newBytes = new byte[len];
						bytes.CopyTo(newBytes, 0);
						bytes = newBytes;
					}

					UInt64 sum = 0;
					for (int i = 0; i < bytes.Length / 4; i++)
					{
						uint val = BitConverter.ToUInt32(bytes, i * 4);
						sum += val;
					}
					Console.WriteLine("file {0} sum = {1}", filename, sum);
				}
			}
		}
	}

	class FileHandlerCommandHelp : FileHandlerCommand
	{
		public override void Execute()
		{
			Console.WriteLine("Help");
			Console.WriteLine("[-f] <filename> Specify the filename for processing");
			Console.WriteLine("[-m] [find] [-s] <string> Command to a file byte offset specified string");
			Console.WriteLine("[-m] [checksum] It displays the sum of all 32 bit words in the file");
			Console.WriteLine("[-h] Displays help on commands and parameters,");
		}
	}
}
