import sys

def position(host):
    splitted_host = host.split("\t")
    ip = splitted_host[0]
    splitted_ip = ip.split(".")
    return int(splitted_ip[2]) & 0xf

possible_moves = []
hosts_file = open(sys.argv[1], "rt")
hosts = hosts_file.readlines()
for host in hosts:
    splitted_host = host.split("\t")
    ip = splitted_host[0]
    domain = splitted_host[1]
    domain = domain.strip("\n")
    splitted_ip = ip.split(".")
    if (int(splitted_ip[0]) == 0x7f) and ((int(splitted_ip[3]) & 1) == 0):
        possible_moves.append(host)

possible_moves.sort(key=position)
for move in possible_moves:
    print(move)
