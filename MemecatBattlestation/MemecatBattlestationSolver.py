encp = '\x03 &$-\x1e\x02 //./'
password = ""
for i in range(0, len(encp)):
    password += chr(ord(encp[i]) ^ 0x41)
print password
