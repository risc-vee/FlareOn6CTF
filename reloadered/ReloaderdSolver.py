encoded = b"\x7A\x17\x08\x34\x17\x31\x3B\x25\x5B\x18\x2E\x3A\x15\x56\x0E\x11\x3E\x0D\x11\x3B\x24\x21\x31\x06\x3C\x26\x7C\x3C\x0D\x24\x16\x3A\x14\x79\x01\x3A\x18\x5A\x58\x73\x2E\x09\x00\x16\x00\x49\x22\x01\x40\x08\x0A\x14"
to_decode = bytes(encoded[39:])
flag_suffix = "@flare-on.com".encode()
key = []
index = 0
for b in to_decode:
    key.append(flag_suffix[index] ^ to_decode[index])
    index += 1

flag = ""
block_len = len(flag_suffix)
decode_i = 0
key_i = 0
while decode_i < len(encoded):
    decode_block = encoded[decode_i:]
    for key_i in range(0, block_len):
        flag += chr(decode_block[key_i] ^ key[key_i])
        key_i += 1
    key_i = 0
    decode_i += block_len
print(flag)