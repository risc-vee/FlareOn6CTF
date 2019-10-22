import z3

h = [ 0xA7, 0xBF, 0xD2, 0x9E, 0x0F, 0x01, 0x6B, 0x53, 0x68, 0x37, 0xB7, 0x60, 0x7C, 0xBA, 0xB4, 0xA8 ]
xor = [212, 162, 242, 218, 101, 109, 50, 31, 125, 112, 249, 83, 55, 187, 131, 206]
h = [h[i] ^ xor[i] for i in range(16)]
x0, x1, x2, x3, x4, x5, x6, x7, x8, x9, x10, x11, x12, x13, x14, x15 = z3.BitVecs('x0 x1 x2 x3 x4 x5 x6 x7 x8 x9 x10 x11 x12 x13 x14 x15', 8)

s = z3.Solver()
s.add(z3.And(
    (h[0] == x2 ^ x3 ^ x4 ^ x8 ^ x11 ^ x14),
    (h[1] == x0 ^ x1 ^ x8 ^ x11 ^ x13 ^ x14),
    (h[2] == x0 ^ x1 ^ x2 ^ x4 ^ x5 ^ x8 ^ x9 ^ x10 ^ x13 ^ x14 ^ x15),
    (h[3] == x5 ^ x6 ^ x8 ^ x9 ^ x10 ^ x12 ^ x15),
    (h[4] == x1 ^ x6 ^ x7 ^ x8 ^ x12 ^ x13 ^ x14 ^ x15),
    (h[5] == x0 ^ x4 ^ x7 ^ x8 ^ x9 ^ x10 ^ x12 ^ x13 ^ x14 ^ x15),
    (h[6] == x1 ^ x3 ^ x7 ^ x9 ^ x10 ^ x11 ^ x12 ^ x13 ^ x15),
    (h[7] == x0 ^ x1 ^ x2 ^ x3 ^ x4 ^ x8 ^ x10 ^ x11 ^ x14),
    (h[8] == x1 ^ x2 ^ x3 ^ x5 ^ x9 ^ x10 ^ x11 ^ x12),
    (h[9] == x6 ^ x7 ^ x8 ^ x10 ^ x11 ^ x12 ^ x15),
    (h[10] == x0 ^ x3 ^ x4 ^ x7 ^ x8 ^ x10 ^ x11 ^ x12 ^ x13 ^ x14 ^ x15),
    (h[11] == x0 ^ x2 ^ x4 ^ x6 ^ x13),
    (h[12] == x0 ^ x3 ^ x6 ^ x7 ^ x10 ^ x12 ^ x15),
    (h[13] == x2 ^ x3 ^ x4 ^ x5 ^ x6 ^ x7 ^ x11 ^ x12 ^ x13 ^ x14),
    (h[14] == x1 ^ x2 ^ x3 ^ x5 ^ x7 ^ x11 ^ x13 ^ x14 ^ x15),
    (h[15] == x1 ^ x3 ^ x5 ^ x9 ^ x10 ^ x11 ^ x13 ^ x15),
))


if s.check() == z3.sat:
    m = s.model()
    launch_code = ""
    #Be lazy and create your own hacks ;)
    launch_code += (chr(int(str(m[x0]))))
    launch_code += (chr(int(str(m[x1]))))
    launch_code += (chr(int(str(m[x2]))))
    launch_code += (chr(int(str(m[x3]))))
    launch_code += (chr(int(str(m[x4]))))
    launch_code += (chr(int(str(m[x5]))))
    launch_code += (chr(int(str(m[x6]))))
    launch_code += (chr(int(str(m[x7]))))
    launch_code += (chr(int(str(m[x8]))))
    launch_code += (chr(int(str(m[x9]))))
    launch_code += (chr(int(str(m[x10]))))
    launch_code += (chr(int(str(m[x11]))))
    launch_code += (chr(int(str(m[x12]))))
    launch_code += (chr(int(str(m[x13]))))
    launch_code += (chr(int(str(m[x14]))))
    launch_code += (chr(int(str(m[x15]))))
    print launch_code
else:
    print "unsat"
