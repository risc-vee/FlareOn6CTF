package nesrom.format;
import java.security.InvalidParameterException;

public class Utils {
	
	public static int kiloByte(int KBs)
	{
		return KBs * 1024;
	}
	
	public static byte[] memCopy(byte[] src, int len)
	{
		if (len <= 0) {
			throw new InvalidParameterException(String.format("Invalid buffer length: %d", len));
		}
		byte[] dst = new byte[len];
		for (int i = 0; i < len; i++)
		{
			dst[i] = src[i];
		}
		return dst;
	}
	
	public static void hexDump(byte[] src, int offset, int len)
	{
		for (int i = 0; i < len; i++)
		{
			System.out.print(String.format("0x%X ", src[offset + i]));
		}
		System.out.print("\r\n");
	}
}
