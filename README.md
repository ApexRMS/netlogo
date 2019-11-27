# netlogo
SyncroSim Base Package providing a scenario based interface for running the NetLogo agent-based modelling environment.

## Overview

The [SyncroSim](https://syncrosim.com/) netlogo package is designed to provide an interface for the [NetLogo model](https://ccl.northwestern.edu/netlogo/ ). NetLogo is a multi agent programmable environment for agent based simulation modelling.  The netlogo package for SyncroSim allows users to structure scenario input and output data, run the model and explore model outputs using either a graphical user interface (GUI), a command line interface or the [rsyncrosim](https://syncrosim.com/r-package/) package for [R](https://www.r-project.org/). The initial version of the netlogo package was written against NetLogo version 6.1.1.

## Installation Instructions

1. [Download](https://ccl.northwestern.edu/netlogo/download.shtml) and install NetLogo (Note: a Java runtime environment is required to run NetLogo)
2. [Install SyncroSim](http://docs.syncrosim.com/getting_started/inst_win.html).
3. Install the SyncroSim netlogo package ([example of how to install a package](http://docs.syncrosim.com/getting_started/quickstart.html#step-1---install-the-demo-sales-package))

## Getting Started

### Configure Library Properties

1. Open the SyncroSim GUI and select **File | New**.
2. Select the **netlogo** base package and the **NetLogo Example** template; specify a file name and folder location for your netlogo SyncroSim library and click **OK**.
3. The [SyncroSim library explorer](http://docs.syncrosim.com/how_to_guides/library_explorer_overview.html) should now display your new netlogo library.
4. Highlight your netlogo library in the library explorer and select **File | Library Properties**.
5. On the **Options | NetLogo** of the **Library Properties** specify the **NetLogo executable filename** where the NetLogo.exe file is located. For the x64 version of NetLogo This is typically "**C:\Program Files\NetLogo 6.X.X**".
6. In the **Library Properties** for NetLogo you can also select to **Run in window** which will at runtime show the NetLogo GUI.  This is useful for initially debugging your model but can be cumbersome if you are running many model realizations and scenarios.

### View Definitions

1. In the [SyncroSim library explorer](http://docs.syncrosim.com/how_to_guides/library_explorer_overview.html) right click on **Definitions** and select the **Symbols** tab. The netlogo template library has been preloaded with a very simple example agent based model of biocontrol agents or **bugs** looking for and eating an invasive plant on the landscape. The model is written in a *.nlogo code file that ships with the template library.  In this file certain variables defined in the code can be set in the SyncroSim user interface by using **Symbols**.  A **Symbol** is simply a collection of characters that denotes where a specific value should be inserted into the nlogo code file at runtime.  Symbols can be used to set state variables. Symbols can also be used to set input files that need to be loaded by NetLogo at runtime such as a raster specifying the starting landscape conditions.  Details on how **Symbols** are used in the NetLogo code files will be explained in the documentation section on **NetLogo Code**.
2. The last tab in the **Definitions** for the netlogo package is for **Output Variables**.  The example library contains four output variables:  **Closed**, **Open**, and **Invaded** are used to denote the amount of area in these three possible discrete vegetation states. The output variable **landCover** is used to represent a map of the landscape state.  Like Symbols, Output Variables are used in the NetLogo code; their purpose is to allow SyncroSim to load NetLogo output into the model database and to display that output in the user interface.  Further explanation on Output Variables is given in the section below on **NetLogo Code**.

### View Scenario Properties

1. In the [SyncroSim library explorer](http://docs.syncrosim.com/how_to_guides/library_explorer_overview.html) right click on the scenario called **5 Bugs** and select **Open**. This is a scenario used to represent 5 biocontrol agents or **Bugs** moving across the landscape and eating **Invaded** patches.
2. The **Run Control** property for this scenario is set to run 10 timesteps and 7 iterations.  The timesteps will be used to define how many [NetLogo ticks](http://ccl.northwestern.edu/netlogo/docs/dictionary.html#tick) to run each iteration for.  The NetLogo model will be run once per iteration.
3. The **Script** property points to the [netLogoExample.nlogo](https://github.com/ApexRMS/netlogo/blob/master/src/Templates/netlogo-example.ssim.input/Scenario-1/netlogo_Script/netLogoExample.nlogo) code file that comes with the template library. The **Script** property also denotes the NetLogo **Experiment name** which is "experiment" by default in NetLogo.  For more details on NetLogo experiments see the [NetLogo Documentation](https://ccl.northwestern.edu/netlogo/2.0.0/docs/behaviorspace.html) on Behaviour Space.
4. On the **Inputs** property note that you can specify a starting number of **Bugs** for the simulation.
5. On the **Input Files** property you can specify the input ascii raster file that denotes the starting conditions for the landscape.

### Run the NetLogo model

1. In the [SyncroSim library explorer](http://docs.syncrosim.com/how_to_guides/library_explorer_overview.html) right click on the scenario called **5 Bugs** and select **Run**.
2. *If you checked* **Run in window** in the **Options | NetLogo** library property, you will see the NetLogo GUI open and you will need to press **setup** then **go** followed by **File | Quit**. This will be repeated for every model realization.

### View Charts of Output

Once the simulation is complete you can use the [SyncroSim chart window](http://docs.syncrosim.com/how_to_guides/results_chart_window.html) to view NetLogo outputs. Note that you can disaggregate and include output for any of the variables defined in the project **Definitions**. You can also choose to use the [rsyncrosim package](https://syncrosim.com/r-package/) to extract model output into R dataframes.

### View Maps of Output

The example library also contains a map that displays the Land Cover (Open, Closed or Invaded) for each cell at different realizations and time points.  Mapping variables can be either discrete such as in this example or continuous.  To view this map in the SyncroSim GUI you will need to use the [SyncroSim map window](http://docs.syncrosim.com/how_to_guides/results_map_window.html).

## NetLogo Code

To learn how to build your own SyncroSim netlogo model take a look at the [example nlogo code file](https://github.com/ApexRMS/netlogo/blob/master/src/Templates/netlogo-example.ssim.input/Scenario-1/netlogo_Script/netLogoExample.nlogo) provided with the template library.  Note also that if you open this file *as is* with NetLogo you will receive several errors because it contains **Symbols** where SyncroSim will insert values at runtime. 

Note that in the template library **Definitions** there are two symbols **INPUT_RASTER** and **NUM_BUGS**.  You can find these symbols in the [example nlogo code file](https://github.com/ApexRMS/netlogo/blob/master/src/Templates/netlogo-example.ssim.input/Scenario-1/netlogo_Script/netLogoExample.nlogo). For example the number of bugs at runtime will be set using the following code:

```netlogo
to setup-bugs
  create-bugs %NUM_BUGS% [
    set color black
    set shape "arrow"
    set size 2
  ask bugs [ setxy random-xcor random-ycor ]

  ]
end
```

and the initial landscape conditions raster will be loaded using this code:

```netlogo
 set patch-dataset gis:load-dataset %INPUT_RASTER%
```

At runtime SyncroSim will replace the symbols with the values set by the user in the **Scenario Properties**. If you chose the SyncroSim **Library Properties | Options | NetLogo |Run In Window** option you will see that when NetLogo opens a copy of this file at runtime and you can view the code where the symbols will be replaced by the values configured in the SyncroSim UI.

Note also that the **Definitions** contain a tab called **Output Variables**.  These are used to load output into the SyncroSim library once the run is complete.  Output variables can represent either state variables for charting or output raster files for mapping.  In the template example there are three categorical variables used to represent the state of each cell on the landscape: **Open**, **Closed**, and **Invaded**.  There is also a variable called **landCover** that is used for an output asc raster of the landscape state.  You can see examples of how these variables are used in the [example NetLogo code file](https://github.com/ApexRMS/netlogo/blob/master/src/Templates/netlogo-example.ssim.input/Scenario-1/netlogo_Script/netLogoExample.nlogo). For example, in the following lines, a csv file of the amount of area in each of the Output Variable land cover classes is written:

```netlogo
file-open %SSIM_VARIABLE_FILENAME%
  file-print csv:to-row (list %SSIM_ITERATION% ticks "Open" Open)
  file-print csv:to-row (list %SSIM_ITERATION% ticks "Closed" Closed)
  file-print csv:to-row (list %SSIM_ITERATION% ticks "Invaded" Invaded)
  file-flush ; Make sure that the output is saved to file
file-close
```

Each line being printed specifies the current iteration, timestep, output variable name and output variable value.

The following code writes a raster of the **landCover** output variable and prints a record to the csv catalog of output rasters to be loaded into the SyncroSim library.

```netlogo
gis:store-dataset patches_out (word %SSIM_NETLOGO_TEMP_FOLDER% "\\it" %SSIM_ITERATION% ".ts" ticks ".landCover.asc")

file-open %SSIM_VARIABLE_RASTER_FILENAME%
file-print csv:to-row (list %SSIM_ITERATION% ticks "Landscape Cover Map" (word %SSIM_NETLOGO_TEMP_FOLDER% "\\it" %SSIM_ITERATION% ".ts" ticks ".landCover.tif"))
file-close

```

The first part writes an ascii raster to the temporary runtime location.  The second part creates a record of that raster in the csv catalog of output files that will be loaded into SyncroSim once the run is complete.  The csv file contains a column for each iteration, timestep, output variable, and path to the output file.

You will have noticed that in the code examples shown there are other symbols bracketed by % characters.  These are SyncroSim reserved symbols that are described in the next section.

### Reserved Symbols for the SyncroSim netlogo Package

In addition to the symbols created by the user in the **Definitions** there are a number of reserved symbols that you will find in the example nlogo file provided with the template library.  These are values that are automatically set by syncrosim at runtime and you can use these in your code when you create your own SyncroSim netlogo model. The symbols include the following:

```netlogo
  ;Current Iteration %SSIM_ITERATION%
  ;Number of Timesteps per Iteration %SSIM_TICKS%
  ;Output Variable Filename %SSIM_VARIABLE_FILENAME%
  ;Output Variable Raster Filename %SSIM_VARIABLE_RASTER_FILENAME%
  ;Temp Folder Path %SSIM_NETLOGO_TEMP_FOLDER%
```

Note that in this example the number of timesteps to run is inserted by SyncroSim from the scenario **Run Control** property.  Note also that the **Output Variable File** and the **Output Variable Raster File** are csv files with specified formats.  See the code examples above, where these files are being written.  

To use the number of timesteps set in **Run Control** your code must include the following:

```
to setup
  __stdout (word "ssim-task-start=" %SSIM_TICKS%)
  ;_____________________________
  ;INCLUDE OTHER SETUP CODE HERE
  ;_____________________________
  reset-ticks
end
```



```
to go

  if (ticks = %SSIM_TICKS% + 1) [
    __stdout "ssim-task-end=True"
    file-close
    stop
  ]
  ;__________________________
  ;INCLUDE OTHER GO CODE HERE
  ;__________________________
  tick
  __stdout "ssim-task-step=1"
end
```

the calls to _stdout allow you to see the status of the NetLogo run progress on the SyncroSim GUI.
