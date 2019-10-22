//TODO write a description for this script
//@author Malik Ashebani
//@category _NEW_
//@keybinding 
//@menupath 
//@toolbar 

import ghidra.app.plugin.core.functiongraph.SetFormatDialogComponentProvider;
import ghidra.app.script.GhidraScript;
import ghidra.program.model.util.*;
import ghidra.program.model.reloc.*;
import ghidra.program.model.data.*;
import ghidra.program.model.block.*;
import ghidra.program.model.symbol.*;
import ghidra.program.model.scalar.*;
import ghidra.program.model.mem.*;
import ghidra.program.model.listing.*;
import ghidra.program.model.lang.*;
import ghidra.program.model.pcode.*;
import ghidra.program.flatapi.FlatProgramAPI;
import ghidra.program.model.address.*;
import ghidra.app.util.HexLong;

import java.awt.Color;
import java.io.*;

public class NESROMCoverageHighlight extends GhidraScript {

    public void run() throws Exception {
    	FileReader fr = new FileReader("D:\\Trace - snake.txt");
    	BufferedReader br = new BufferedReader(fr);
    	String InstructionTraceLine = br.readLine();
    	while (InstructionTraceLine != null)
    	{
    		String[] Splitted = InstructionTraceLine.split(" ");
    		int Address = 0;
    		try {
    			Address = Integer.parseInt(Splitted[0], 16);
    		}
    		catch (NumberFormatException ex)
    		{
    			InstructionTraceLine = br.readLine();
    			continue;
    		}
    		setBackgroundColor(currentProgram.getAddressFactory().getDefaultAddressSpace().getAddress((long)Address), Color.RED);
    		InstructionTraceLine = br.readLine();
    	}
    	br.close();
    }

}
