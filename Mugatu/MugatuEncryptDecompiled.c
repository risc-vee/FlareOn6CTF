void Encrypt(uint const20h, uint *lpMappedGIFFile, char * lpRecievedMailSlotData)
{
  uint Op1;
  uint SecondDWORD;
  uint Op2;
  uint FirstDWORD;
  
  FirstDWORD = lpMappedGIFFile[0];
  SecondDWORD = lpMappedGIFFile[1];
  Op2 = 0;
  do {
    Op1 = (uint)(unsigned char)(lpRecievedMailSlotData[(Op2 & 3)]) + Op2;
    //Op2 = Op2 + 0x9e3779b9;//0x61C88647 * -1
    Op2 = Op2 - 0x61C88647;

    FirstDWORD = FirstDWORD + ((SecondDWORD << 4 ^ SecondDWORD >> 5) + SecondDWORD ^ Op1);
    SecondDWORD = SecondDWORD + ((FirstDWORD * 0x10 ^ FirstDWORD >> 5) + FirstDWORD ^ (uint)(unsigned char)(lpRecievedMailSlotData[(Op2 >> 0xb & 3)]) + Op2);
    /* Iterate 32 times */
    const20h = const20h - 1;
  } while (const20h != 0);

  lpMappedGIFFile[0] = FirstDWORD;
  lpMappedGIFFile[1] = SecondDWORD;
  return;
}