/* ###
 * IP: GHIDRA
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 * 
 *      http://www.apache.org/licenses/LICENSE-2.0
 * 
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */
package nesrom;

import java.io.IOException;
import java.util.*;

import ghidra.app.util.HexLong;
import ghidra.app.util.Option;
import ghidra.app.util.bin.ByteProvider;
import ghidra.app.util.bin.BinaryReader;
import ghidra.app.util.opinion.BinaryLoader;
import ghidra.app.util.opinion.LoadSpec;
import ghidra.framework.model.DomainObject;
import ghidra.program.model.address.Address;
import ghidra.program.model.lang.Language;
import ghidra.program.model.lang.LanguageCompilerSpecPair;
import ghidra.program.model.lang.LanguageNotFoundException;
import ghidra.util.Msg;
import nesrom.format.iNESROM;
import nesrom.format.iNESROMHeader;

/**
 * TODO: Provide class-level documentation that describes what this loader does.
 */
public class NESROMLoader extends BinaryLoader {
	
	protected iNESROMHeader romHeader = null;
	@Override
	public String getName() {
		return "NES ROM (Mapper 0 - UNROM)";
	}

	@Override
	public Collection<LoadSpec> findSupportedLoadSpecs(ByteProvider provider) throws IOException {
		List<LoadSpec> loadSpecs = new ArrayList<>();

		// TODO: Examine the bytes in 'provider' to determine if this loader can load it.  If it 
		// can load it, return the appropriate load specifications.
		BinaryReader binReader = new BinaryReader(provider, true);
		
		try {
			romHeader = new iNESROMHeader(binReader.readNextByteArray(iNESROMHeader.NESHeaderSize));
		}
		catch (Exception e)
		{
			return loadSpecs;
		}
		loadSpecs.add(new LoadSpec(this, iNESROM.PRGROMLowerBankBaseAddress, new LanguageCompilerSpecPair("6502:LE:16:NES", "default"), true));
		return loadSpecs;
	}
	
	@Override
	public List<Option> getDefaultOptions(ByteProvider provider, LoadSpec loadSpec,
			DomainObject domainObject, boolean isLoadIntoProgram) {
		List<Option> options =
			super.getDefaultOptions(provider, loadSpec, domainObject, isLoadIntoProgram);
		
		LanguageCompilerSpecPair langCompilerSpecPair = loadSpec.getLanguageCompilerSpec();
		Language importerLanguage = null;
		try {
			importerLanguage = getLanguageService().getLanguage(langCompilerSpecPair.languageID);
		} catch (LanguageNotFoundException e) {
			Msg.warn(this, e.getMessage(), e);
		}
		long PRGROMBaseAddress = iNESROM.PRGROMLowerBankBaseAddress;
		if (romHeader.getPRGROMUnits() == 1)
		{
			PRGROMBaseAddress = iNESROM.PRGROMHigherBankBaseAddress;
		}
		Address baseAddress = importerLanguage.getAddressFactory().getDefaultAddressSpace().getAddress(PRGROMBaseAddress);
		
		Option option0 = options.get(0);
		option0.setValue("PRG_ROM");
		options.set(0, option0);
		
		Option option1 = options.get(1);
		option1.setValue(baseAddress);
		options.set(1, option1);
		
		Option option2 = options.get(2);
		option2.setValue(new HexLong(iNESROMHeader.NESHeaderSize));
		options.set(2, option2);
		
		Option option3 = options.get(3);
		
		option3.setValue(new HexLong(romHeader.getPRGROMSize()));
		options.set(3, option3);
		
		return options;
	}
}
