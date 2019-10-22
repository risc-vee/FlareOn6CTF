using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vv_max
{
    class VVMaxDisassembler
    {
        protected byte[] _Bytecode;
        protected Dictionary<byte, string> _InstructionSet = new Dictionary<byte, string>();
        protected Dictionary<byte, string> _OpcodeHandlersAddresses = new Dictionary<byte, string>();
        protected const ulong _BaseAddress = 0x0;
        protected const uint _Arg1Base = 0x03;
        protected const uint _Arg2Base = 0x25;
        protected const uint _VMHandlersBase = 0x0C08;
        protected const uint _PCBase = 0x0C00;
        protected const uint _DoubleQuadwordsMemoryBase = 0x0800;
        protected const uint _DoubleQuadwordsCount = 0x20;

        public VVMaxDisassembler(byte[] Bytecode)
        {
            _InstructionSet.Add(0x00, "pzero_dqs");
            _OpcodeHandlersAddresses.Add(0x00, "0x1400017B0");

            _InstructionSet.Add(0x01, "pmul_addubsw_dq");
            _OpcodeHandlersAddresses.Add(0x01, "0x140002300");

            _InstructionSet.Add(0x02, "pmul_adduw_dq");
            _OpcodeHandlersAddresses.Add(0x02, "0x1400021E0");

            _InstructionSet.Add(0x03, "pxor_dq");
            _OpcodeHandlersAddresses.Add(0x03, "0x140003030");

            _InstructionSet.Add(0x04, "por_dq");
            _OpcodeHandlersAddresses.Add(0x04, "0x140002740");

            _InstructionSet.Add(0x05, "pand_dq");
            _OpcodeHandlersAddresses.Add(0x05, "0x140001DD0");

            _InstructionSet.Add(0x07, "paddb_dq");
            _OpcodeHandlersAddresses.Add(0x07, "0x140001CB0");

            _InstructionSet.Add(0x0B, "paddd_dq");
            _OpcodeHandlersAddresses.Add(0x0B, "0x140001A70");

            _InstructionSet.Add(0x11, "pmovq_dq");
            _OpcodeHandlersAddresses.Add(0x11, "0x140002010");

            _InstructionSet.Add(0x12, "pshrd_dq");
            _OpcodeHandlersAddresses.Add(0x12, "0x140002980");

            _InstructionSet.Add(0x13, "pshld_dq");
            _OpcodeHandlersAddresses.Add(0x13, "0x1400020D0");

            _InstructionSet.Add(0x14, "pshufb_dq");
            _OpcodeHandlersAddresses.Add(0x14, "0x140002A90");

            _InstructionSet.Add(0x15, "ppermd_dq");
            _OpcodeHandlersAddresses.Add(0x15, "0x140002860");

            _InstructionSet.Add(0x16, "pcmpeqb_dq");
            _OpcodeHandlersAddresses.Add(0x16, "0x140001EF0");

            _InstructionSet.Add(0x17, "nop");
            _OpcodeHandlersAddresses.Add(0x17, "0x140002600");

            _InstructionSet.Add(0xFF, "halt");

            _Bytecode = Bytecode;
        }

        public IEnumerable<string> Disassemble()
        {
            //uint BytecodeLength = _Bytecode.Length;
            uint PC = 0;
            while (true)
            {
                byte Opcode = _Bytecode[PC];
                if (!_InstructionSet.ContainsKey(Opcode))
                {
                    yield return string.Format("{0:X}: {2:X} | {1}", PC, "Illegal instruction", Opcode);
                    ++PC;
                    continue;
                }
                switch (Opcode)
                {
                    case 0x0:
                        yield return string.Format("{0:X}: {2:X} | {1}", PC, _InstructionSet[Opcode], Opcode);
                        PC += 1;
                        break;
                    case 0x01:
                        //[Base + PC][0]
                        PC += 1;
                        byte pxor_dq_DstDQWOffset = _Bytecode[PC];//Operand1
                        //[Base + PC][1]
                        PC += 1;
                        byte pxor_dq_SrcDQWOffset1 = _Bytecode[PC];//Operand2
                        //[Base + PC][2]
                        PC += 1;
                        byte pxor_dq_SrcDQWOffset2 = _Bytecode[PC];//Operand3
                        PC += 1;
                        yield return string.Format("{0:X}: {5:X} | {1}  [800:{2:X}] = [800:{3:X}], [800:{4:X}]", (PC - 4), _InstructionSet[Opcode], (pxor_dq_DstDQWOffset * 0x20), (pxor_dq_SrcDQWOffset1 * 0x20), (pxor_dq_SrcDQWOffset2 * 0x20), Opcode);
                        break;
                    case 0x02:
                        //[Base + PC][0]
                        PC += 1;
                        byte pmul_addwd_dq_DstDQWOffset = _Bytecode[PC];//Operand1
                        //[Base + PC][1]
                        PC += 1;
                        byte pmul_addwd_dq_SrcDQWOffset1 = _Bytecode[PC];//Operand2
                        //[Base + PC][2]
                        PC += 1;
                        byte pmul_addwd_dq_SrcDQWOffset2 = _Bytecode[PC];//Operand3
                        PC += 1;
                        yield return string.Format("{0:X}: {5:X} | {1}  [800:{2:X}] = [800:{3:X}], [800:{4:X}]", (PC - 4), _InstructionSet[Opcode], (pmul_addwd_dq_DstDQWOffset * 0x20), (pmul_addwd_dq_SrcDQWOffset1 * 0x20), (pmul_addwd_dq_SrcDQWOffset2 * 0x20), Opcode);
                        break;
                    case 0x03:
                        //[Base + PC][0]
                        PC += 1;
                        byte pmul_addubsw_dq_DstDQWOffset = _Bytecode[PC];//Operand1
                        //[Base + PC][1]
                        PC += 1;
                        byte pmul_addubsw_dq_SrcDQWOffset1 = _Bytecode[PC];//Operand2
                        //[Base + PC][2]
                        PC += 1;
                        byte pmul_addubsw_dq_SrcDQWOffset2 = _Bytecode[PC];//Operand3
                        PC += 1;
                        yield return string.Format("{0:X}: {5:X} | {1}  [800:{2:X}] = [800:{3:X}], [800:{4:X}]", (PC - 4), _InstructionSet[Opcode], (pmul_addubsw_dq_DstDQWOffset * 0x20), (pmul_addubsw_dq_SrcDQWOffset1 * 0x20), (pmul_addubsw_dq_SrcDQWOffset2 * 0x20), Opcode);
                        break;
                    case 0x04:
                        //[Base + PC][0]
                        PC += 1;
                        byte por_dq_DstDQWOffset = _Bytecode[PC];//Operand1
                        //[Base + PC][1]
                        PC += 1;
                        byte por_dq_SrcDQWOffset1 = _Bytecode[PC];//Operand2
                        //[Base + PC][2]
                        PC += 1;
                        byte por_dq_SrcDQWOffset2 = _Bytecode[PC];//Operand3
                        PC += 1;
                        yield return string.Format("{0:X}: {5:X} | {1}  [800:{2:X}] = [800:{3:X}], [800:{4:X}]", (PC - 4), _InstructionSet[Opcode], (por_dq_DstDQWOffset * 0x20), (por_dq_SrcDQWOffset1 * 0x20), (por_dq_SrcDQWOffset2 * 0x20), Opcode);
                        break;
                    case 0x05:
                        //[Base + PC][0]
                        PC += 1;
                        byte pand_dq_DstDQWOffset = _Bytecode[PC];//Operand1
                        //[Base + PC][1]
                        PC += 1;
                        byte pand_dq_SrcDQWOffset1 = _Bytecode[PC];//Operand2
                        //[Base + PC][2]
                        PC += 1;
                        byte pand_dq_SrcDQWOffset2 = _Bytecode[PC];//Operand3
                        PC += 1;
                        yield return string.Format("{0:X}: {5:X} | {1}  [800:{2:X}] = [800:{3:X}], [800:{4:X}]", (PC - 4), _InstructionSet[Opcode], (pand_dq_DstDQWOffset * 0x20), (pand_dq_SrcDQWOffset1 * 0x20), (pand_dq_SrcDQWOffset2 * 0x20), Opcode);
                        break;
                    case 0x07:
                        //[Base + PC][0]
                        PC += 1;
                        byte paddb_dq_DstDQWOffset = _Bytecode[PC];//Operand1
                        //[Base + PC][1]
                        PC += 1;
                        byte paddb_dq_SrcDQWOffset1 = _Bytecode[PC];//Operand2
                        //[Base + PC][2]
                        PC += 1;
                        byte paddb_dq_SrcDQWOffset2 = _Bytecode[PC];//Operand3
                        PC += 1;
                        yield return string.Format("{0:X}: {5:X} | {1}  [800:{2:X}] = [800:{3:X}], [800:{4:X}]", (PC - 4), _InstructionSet[Opcode], (paddb_dq_DstDQWOffset * 0x20), (paddb_dq_SrcDQWOffset1 * 0x20), (paddb_dq_SrcDQWOffset2 * 0x20), Opcode);
                        break;
                    case 0x0B:
                        //[Base + PC][0]
                        PC += 1;
                        byte paddd_dq_DstDQWOffset = _Bytecode[PC];//Operand1
                        //[Base + PC][1]
                        PC += 1;
                        byte paddd_dq_SrcDQWOffset1 = _Bytecode[PC];//Operand2
                        //[Base + PC][2]
                        PC += 1;
                        byte paddd_dq_SrcDQWOffset2 = _Bytecode[PC];//Operand3
                        PC += 1;
                        yield return string.Format("{0:X}: {5:X} | {1}  [800:{2:X}] = [800:{3:X}], [800:{4:X}]", (PC - 4), _InstructionSet[Opcode], (paddd_dq_DstDQWOffset * 0x20), (paddd_dq_SrcDQWOffset1 * 0x20), (paddd_dq_SrcDQWOffset2 * 0x20), Opcode);
                        break;
                    case 0x11:
                        //[Base + PC][0]
                        PC += 1;
                        byte pmovq_dq_DstDQOffset = _Bytecode[PC];//Operand1
                        //[Base + PC][1]
                        PC += 1;
                        ulong pmovq_dq_SrcDQOffset = PC;//Operand2
                        yield return string.Format("{0:X}: {4:X} | {1}  [800:{2:X}], [000:{3:X}]", (PC - 2), _InstructionSet[Opcode], (pmovq_dq_DstDQOffset * 0x20), pmovq_dq_SrcDQOffset, Opcode);
                        PC += 0x20;
                        break;
                    case 0x12:
                        //[Base + PC][0]
                        PC += 1;
                        byte pshrd_dq_DstDQWOffset = _Bytecode[PC];//Operand1
                        //[Base + PC][1]
                        PC += 1;
                        byte pshrd_dq_SrcDQWOffset1 = _Bytecode[PC];//Operand2
                        //[Base + PC][2]
                        PC += 1;
                        byte pshrd_dq_SrcDQWOffset2 = _Bytecode[PC];//Operand3
                        PC += 1;
                        yield return string.Format("{0:X}: {5:X} | {1}  [800:{2:X}] = [800:{3:X}], {4:X}", (PC - 4), _InstructionSet[Opcode], (pshrd_dq_DstDQWOffset * 0x20), (pshrd_dq_SrcDQWOffset1 * 0x20), (pshrd_dq_SrcDQWOffset2), Opcode);
                        break;
                    case 0x13:
                        //[Base + PC][0]
                        PC += 1;
                        byte pshld_dq_DstDQWOffset = _Bytecode[PC];//Operand1
                        //[Base + PC][1]
                        PC += 1;
                        byte pshld_dq_SrcDQWOffset1 = _Bytecode[PC];//Operand2
                        //[Base + PC][2]
                        PC += 1;
                        byte pshld_dq_SrcDQWOffset2 = _Bytecode[PC];//Operand3
                        PC += 1;
                        yield return string.Format("{0:X}: {5:X} | {1}  [800:{2:X}] = [800:{3:X}], {4:X}", (PC - 4), _InstructionSet[Opcode], (pshld_dq_DstDQWOffset * 0x20), (pshld_dq_SrcDQWOffset1 * 0x20), (pshld_dq_SrcDQWOffset2), Opcode);
                        break;
                    case 0x14:
                        //[Base + PC][0]
                        PC += 1;
                        byte shuff_dqw_DstDQWOffset = _Bytecode[PC];//Operand1
                        //[Base + PC][1]
                        PC += 1;
                        byte shuff_dqw_SrcDQWOffset1 = _Bytecode[PC];//Operand2
                        //[Base + PC][2]
                        PC += 1;
                        byte shuff_dqw_SrcDQWOffset2 = _Bytecode[PC];//Operand3
                        PC += 1;
                        yield return string.Format("{0:X}: {5:X} | {1}  [800:{2:X}] = [800:{3:X}], [800:{4:X}]", (PC - 4), _InstructionSet[Opcode], (shuff_dqw_DstDQWOffset * 0x20), (shuff_dqw_SrcDQWOffset1 * 0x20), (shuff_dqw_SrcDQWOffset2 * 0x20), Opcode);
                        break;
                    case 0x15:
                        //[Base + PC][0]
                        PC += 1;
                        byte perm_dqw_DstDQWOffset = _Bytecode[PC];//Operand1
                        //[Base + PC][1]
                        PC += 1;
                        byte perm_dqw_SrcDQWOffset1 = _Bytecode[PC];//Operand2
                        //[Base + PC][2]
                        PC += 1;
                        byte perm_dqw_SrcDQWOffset2 = _Bytecode[PC];//Operand3
                        PC += 1;
                        yield return string.Format("{0:X}: {5:X} | {1}  [800:{2:X}] = [800:{3:X}], [800:{4:X}]", (PC - 4), _InstructionSet[Opcode], (perm_dqw_DstDQWOffset * 0x20), (perm_dqw_SrcDQWOffset1 * 0x20), (perm_dqw_SrcDQWOffset2 * 0x20), Opcode);
                        break;
                    case 0x16:
                        //[Base + PC][0]
                        PC += 1;
                        byte cmpeqb_dqw_DstDQWOffset = _Bytecode[PC];//Operand1
                        //[Base + PC][1]
                        PC += 1;
                        byte cmpeqb_dqw_Op1DQWOffset = _Bytecode[PC];//Operand2
                        //[Base + PC][2]
                        PC += 1;
                        byte cmpeqb_dqw_Op2DQWOffset = _Bytecode[PC];//Operand3
                        PC += 1;
                        yield return string.Format("{0:X}: {5:X} | {1}  [800:{2:X}] = [800:{3:X}], [800:{4:X}]", (PC - 4), _InstructionSet[Opcode], (cmpeqb_dqw_DstDQWOffset * 0x20), (cmpeqb_dqw_Op1DQWOffset * 0x20), (cmpeqb_dqw_Op2DQWOffset * 0x20), Opcode);
                        break;
                    case 0x17:
                        yield return string.Format("{0:X}: {2:X} | {1}", PC, _InstructionSet[Opcode], Opcode);
                        PC += 1;
                        break;
                    case 0xFF:
                        yield return string.Format("{0:X}: {2:X} | {1}", PC, _InstructionSet[Opcode], Opcode);
                        PC += 1;
                        yield break;
                }
            }
        }
    }
}
