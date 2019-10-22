package nesrom.format;
import java.io.IOException;


public class iNESROM {
	
	protected byte[] _ROMNESContents = null;
	protected iNESROMHeader _ROMHeader = null;
	public final static int PRGROMLowerBankBaseAddress = 0x8000;
	public final static int PRGROMHigherBankBaseAddress = 0xC000;
	
	public iNESROM(byte[] ROMFileContents) throws IOException
	{
		if (ROMFileContents == null || ROMFileContents.length < iNESROMHeader.NESHeaderSize)
		{
			throw new IOException("Invalid file (Empty or null)");
		}
		_ROMNESContents = ROMFileContents;
		parseHeader();
	}
	
	protected void parseHeader()
	{
		byte[] ROMHeader = Utils.memCopy(_ROMNESContents, iNESROMHeader.NESHeaderSize);
		_ROMHeader = new iNESROMHeader(ROMHeader);
	}
	
	public iNESROMHeader getiNESROMHeader()
	{
		return _ROMHeader;
	}
	
	public int getTrainerAreaOffset()
	{
		return iNESROMHeader.NESHeaderSize;
	}
	
	public int getPRGROMOffset()
	{
		int PRGROMOffset = 0;
		PRGROMOffset += iNESROMHeader.NESHeaderSize;
		if (_ROMHeader.hasTrainerArea())
		{
			PRGROMOffset += iNESROMHeader.TRAINER_AREA_SIZE;
		}
		return PRGROMOffset;
	}
	
	public int getCHRROMOffset()
	{
		int CHRROMOffset = getPRGROMOffset();
		CHRROMOffset += _ROMHeader.getPRGROMSize();
		return CHRROMOffset;
	}
}
