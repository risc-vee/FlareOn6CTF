#include <idc.idc>
static main(void)
{
    auto funcAddr = 0x0;
    auto callerFunctionAddr = 0x0;
    auto funcName = "";
    auto importsFilePath = AskFile(0, "*.txt", "Please select an imports file: ");
    auto fp = fopen(importsFilePath, "r");
    
    do {
         funcAddr = xtol(readstr(fp));
         funcName = readstr(fp);
         if (funcName != -1)
         {
            callerFunctionAddr = DfirstB(funcAddr);
            Message(ltoa(callerFunctionAddr, 16) + "\r\n");
            Jump(callerFunctionAddr);
            auto retCode = MakeNameEx(callerFunctionAddr, funcName, 1);
         }
    } while (funcName != -1);
}