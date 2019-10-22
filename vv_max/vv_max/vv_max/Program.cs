using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace vv_max
{
    class Program
    {
        static void Main(string[] args)
        {
            byte[] Bytecode = File.ReadAllBytes(Path.GetFullPath(args[0]);
            VVMaxDisassembler VMDisassembler = new VVMaxDisassembler(Bytecode);
            foreach (string Instruction in VMDisassembler.Disassemble())
            {
                Console.WriteLine(Instruction);
            }
        }
    }
}
