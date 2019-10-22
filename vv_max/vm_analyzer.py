import flareqdb as fqd

bytecode_base = 0x000000000014EB00
PC_offset = 0x0C00
DQs_offset = 0x0800
ResultOperand = { 'result': None}
def hexdump(data, element_per_line, address):
    c = 1
    line = ""
    #line = "{0:X}: ".format(address)
    for e in data:
        elem = "{0:X}".format(e)
        if e < 0x10:
            elem = "0" + elem
        line += elem + " "
        if c % element_per_line == 0:
            print line
            line = ""
        c += 1
    pass

opcodes = {
    0x00: "pzero_dqs",
    0x01: "pmul_addubsw_dq",
    0x02: "pmul_adduw_dq",
    0x03: "pxor_dq",
    0x04: "por_dq",
    0x05: "pand_dq",
    0x07: "paddb_dq",
    0x0B: "paddd_dq",
    0x11: "pmovq_dq",
    0x12: "pshrd_dq",
    0x13: "pshld_dq",
    0x14: "pshufb_dq",
    0x15: "ppermd_dq",
    0x16: "pcmpeqb_dq",
    0xFF: "halt"
}

opcodes_handlers = {
    #Opcode: (OpernadsHandler, ResultHandler)
    0x00: (0x1400017B0, 0x140001821),
    0x01: (0x140002300, 0x140002414),
    0x02: (0x1400021E0, 0x1400022F4),
    0x03: (0x140003030, 0x140003144),
    0x04: (0x140002740, 0x140002854),
    0x05: (0x140001DD0, 0x140001EE4),
    0x07: (0x140001CB0, 0x140001DC4),
    0x0B: (0x140001A70, 0x140001B84),
    0x11: (0x140002010, 0x1400020C8),
    0x12: (0x140002980, 0x140002A89),
    0x13: (0x1400020D0, 0x1400021D9),
    0x14: (0x140002A90, 0x140002BA4),
    0x15: (0x140002860, 0x140002974),
    0x16: (0x140001EF0, 0x140002004)
}

ThreeAFOpcodes = [
    0x01,
    0x02,
    0x03,
    0x04,
    0x05,
    0x07,
    0x0B,
    #0x12,
    #0x13,
    0x14,
    0x15,
    0x16
]

def ThreeAFOpcodeOperandsHandler(p, q, trace, **kwargs):#three address form
    #q -> debugger
    #q = fqd.Qdb(q)
    opcode = q.r('rax')#int()
    PC = q.dq(bytecode_base + PC_offset)[0]
    Operand1 = q.db(bytecode_base + PC + 1, 1)[0] * 0x20
    p['result'] = Operand1
    Operand2 = q.db(bytecode_base + PC + 2, 1)[0] * 0x20
    Operand3 = q.db(bytecode_base + PC + 3, 1)[0] * 0x20
    print("[PC:{0:X}]: {1} [800:{2:X}] = [800:{3:X}], [800:{4:X}]".format(PC, opcodes[opcode], Operand1, Operand2, Operand3))
    
    Op2Addr = bytecode_base + DQs_offset + Operand2
    DQ = q.db(Op2Addr, 32)
    print "[800:{0:X}]:".format(Operand2)
    hexdump(DQ, 16, Op2Addr)
    
    Op3Addr = bytecode_base + DQs_offset + Operand3
    DQ = q.db(Op3Addr, 32)
    print "[800:{0:X}]:".format(Operand3)
    hexdump(DQ, 16, Op3Addr)
    pass

def ThreeAFOpcodeResultHandler(p, q, trace, **kwargs):#three address form
    #q -> debugger
    #q = fqd.Qdb(q)    
    addr = bytecode_base + DQs_offset + p['result']
    DQ = q.db(addr, 32)
    print "[800:{0:X}]:".format(p['result'])
    hexdump(DQ, 16, addr)
    print "=" * 47
    pass

def PSHIFTDOperandsHandler(p, q, trace, **kwargs):
    #q -> debugger
    #q = fqd.Qdb(q)
    opcode = q.r('rax')#int()
    PC = q.dq(bytecode_base + PC_offset)[0]
    Operand1 = q.db(bytecode_base + PC + 1, 1)[0] * 0x20
    p['result'] = Operand1
    Operand2 = q.db(bytecode_base + PC + 2, 1)[0] * 0x20
    Operand3 = q.db(bytecode_base + PC + 3, 1)[0]
    print("[PC:{0:X}]: {1} [800:{2:X}] = [800:{3:X}], {4:X}".format(PC, opcodes[opcode], Operand1, Operand2, Operand3))
    
    Op2Addr = bytecode_base + DQs_offset + Operand2
    DQ = q.db(Op2Addr, 32)
    print "[800:{0:X}]:".format(Operand2)
    hexdump(DQ, 16, Op2Addr)
    pass

def PSHIFTDResultHandler(p, q, trace, **kwargs):
    #q -> debugger
    #q = fqd.Qdb(q)    
    addr = bytecode_base + DQs_offset + p['result']
    DQ = q.db(addr, 32)
    print "[800:{0:X}]:".format(p['result'])
    hexdump(DQ, 16, addr)
    print "=" * 47
    pass

def PMOVQOperandsHandler(p, q, trace, **kwargs):
    #q -> debugger
    #q = fqd.Qdb(q)
    opcode = q.r('rax')
    PC = q.dq(bytecode_base + PC_offset)[0]
    Operand1 = q.db(bytecode_base + PC + 1, 1)[0] * 0x20
    p['result'] = Operand1
    Operand2 = PC + 2#q.db(bytecode_base + PC + 2, 1)[0]
    print("[PC:{0:X}]: {1} [800:{2:X}], [000:{3:X}]".format(PC, opcodes[opcode], Operand1, Operand2))
    #hexdump(q.db(bytecode_base + PC + 2))
    pass

def PMOVQResultHandler(p, q, trace, **kwargs):
    #q -> debugger
    #q = fqd.Qdb(q)    
    addr = bytecode_base + DQs_offset + p['result']
    DQ = q.db(addr, 32)
    print "[800:{0:X}]:".format(p['result'])
    hexdump(DQ, 16, addr)
    print "=" * 47
    pass

dbg = fqd.Qdb()

for Opcode in ThreeAFOpcodes:
    Handler = opcodes_handlers[Opcode]
    dbg.add_query(Handler[0], ThreeAFOpcodeOperandsHandler)
    dbg.add_query(Handler[1], ThreeAFOpcodeResultHandler)
dbg.add_query(opcodes_handlers[0x11][0], PMOVQOperandsHandler)#0x11: "pmovq_dq"
dbg.add_query(opcodes_handlers[0x11][1], PMOVQResultHandler)#0x11: "pmovq_dq"
dbg.add_query(opcodes_handlers[0x12][0], PSHIFTDOperandsHandler)#0x12: "pshrd_dq"
dbg.add_query(opcodes_handlers[0x12][1], PSHIFTDResultHandler)#0x12: "pshrd_dq"
dbg.add_query(opcodes_handlers[0x13][0], PSHIFTDOperandsHandler)#0x13: "pshl_dq"
dbg.add_query(opcodes_handlers[0x13][1], PSHIFTDResultHandler)#0x13: "pshl_dq"
parts = [ "0", "1", "2", "3", "4", "5", "6", "7" ]
parts = [ "/", "/", "/", "/", "/", "/", "/", "/" ]
arg2 = "{0}{1}{2}{3}{4}{5}{6}{7}".format(parts[0] * 4, parts[1] * 4, parts[2] * 4, parts[3] * 4, parts[4] * 4, parts[5] * 4, parts[6] * 4, parts[7] * 4)

dbg.run(".\\vv_max_no_aslr.exe FLARE2019 {0}".format(arg2), ResultOperand)