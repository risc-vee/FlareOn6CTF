package nesrom.format;
import java.security.InvalidParameterException;

public class iNESROMHeader {
	//header fields////
	protected byte[] Magic;
	protected byte PRG_ROM_UNITS;
	protected byte CHR_ROM_UNITS;
	protected byte Flags6;
	protected byte Flags7;
	protected byte Flags8;
	protected byte Flags9;
	protected byte Flags10;
	protected byte Flags11;
	protected byte Flags12;
	protected byte Flags13;
	protected byte Flags14;
	protected byte Flags15;
	///////////////////
	
	public static enum MirroringDirection {
		Vertical,		// Horizontal mirroring (CIRAM A10 = PPU A11)
		Horizontal	// Vertical mirroring (CIRAM A10 = PPU A10)
	}
	public static enum NESFormatVersion { 
		archaic_iNES, iNES, NES2 
	}
	
	public final static String NES_MAGIC = new String(new byte[] { 0x4E, 0x45, 0x53, 0x1A });
	public final static int NESHeaderSize = 16;
	public final static int TRAINER_AREA_SIZE = 512;
	public final static int PRG_ROM_BANK_SIZE = Utils.kiloByte(16);
	public final static int CHR_ROM_BANK_SIZE = Utils.kiloByte(8);
	
	public iNESROMHeader(byte[] ROMHeader)
	{
		if (ROMHeader.length != NESHeaderSize)
		{
			throw new InvalidParameterException("Header's size must be 16 bytes");
		}
		parse(ROMHeader);
		if (!isValidMagicString(Magic))
		{
			throw new InvalidParameterException("Invalid Magic Signture");
		}
		if (getFormatVersion() != NESFormatVersion.iNES)
		{
			throw new InvalidParameterException(String.format("%s version is not supported", getFormatVersion()));
		}
	}
	
	public NESFormatVersion getFormatVersion()
	{
		boolean isiNES = (Flags11 & Flags12 & Flags13 & Flags14 & Flags15) == 0;
		NESFormatVersion formatVersion = NESFormatVersion.archaic_iNES;
		if ((Flags7 & 0x0C) == 0x08)
		{
			formatVersion = NESFormatVersion.NES2;
		}
		if ((Flags7 & 0x0C) == 0x00 && isiNES)
		{
			formatVersion = NESFormatVersion.iNES;
		}
		return formatVersion;
	}
	
	public static boolean isValidMagicString(byte[] Magic)
	{
		return NES_MAGIC.equals(new String(Magic));
	}
	
	private void parse(byte[] ROMHeader)
	{
		Magic = Utils.memCopy(ROMHeader, NES_MAGIC.length());
		
		PRG_ROM_UNITS = ROMHeader[4];
		CHR_ROM_UNITS = ROMHeader[5];
		
		Flags6 = ROMHeader[6];
		Flags7 = ROMHeader[7];
		Flags8 = ROMHeader[8];
		Flags9 = ROMHeader[9];
		Flags10 = ROMHeader[10];
		Flags11 = ROMHeader[11];
		Flags12 = ROMHeader[12];
		Flags13 = ROMHeader[13];
		Flags14 = ROMHeader[14];
		Flags15 = ROMHeader[15];
	}
	
	public byte[] getMagic()
	{
		return Magic;
	}
	
	public byte getPRGROMUnits()
	{
		return PRG_ROM_UNITS;
	}
	
	public int getPRGROMSize()
	{
		return PRG_ROM_UNITS * PRG_ROM_BANK_SIZE;
	}
	
	public byte getCHRROMUnits()
	{
		return CHR_ROM_UNITS;
	}
	
	public int getCHRROMSize()
	{
		return CHR_ROM_UNITS * CHR_ROM_BANK_SIZE;
	}
	
	public byte getFlags6()
	{
		return Flags6;
	}
	
	public MirroringDirection getMirroringDirection()
	{
		if ((Flags6 & 0x01) == 0x01)
		{
			return MirroringDirection.Vertical;
		}
		return MirroringDirection.Horizontal;
	}
	
	public boolean hasPRGRAM()//battery-backed SRAM (Save RAM)
	{
		return ((Flags6 & 0x02) == 0x02);
	}
	
	public boolean usesCHRRAM()
	{
		return CHR_ROM_UNITS == 0;
	}
	
	public boolean hasTrainerArea()
	{
		return ((Flags6 & 0x04) == 0x04);
	}
	
	public boolean supportsFourScreenArrangement()
	{
		return ((Flags6 & 0x08) == 0x08);
	}
	
	public int getMapperNumber()
	{
		return ((Flags7 & 0xF0) | ((Flags6 & 0xF0) >> 0x04));
	}
}
