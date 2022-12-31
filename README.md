# Pioneer-CLI
CLI to control Pioneer CDJ devices

# Description
Pioneer CLI is a command line interface tool which allows you to control Pioneer devices using a computer. This project was born with the idea of hacking this devices to make cool stuff and understand what is going on.
Pioneer high-end products uses an Ethernet connection to comunicate each other. It uses the UDP protocol to send information around the network about device current status, if there is a track loaded or to request some stuff.

This project is possible thanks to Deep-Symmetry project which builds an entire [documentation](https://djl-analysis.deepsymmetry.org/djl-analysis/packets.html) about the protocol used by those devices. This project was tested using 2 CDJ-2000NXS2 devices and a DJM-900.

# Project information
This project is divided by 3 repositories:
- **ProLinkLib** is a custom C# library developed from scratch which can interact with the Pioneer devices, sending commands an retrieve information about them.
- **PCLILogger** is a Log server which logs all the packets sent and received by ProLinkLib
- **Pioneer CLI** is a command-line tool which the user can interact with the Pioneer devices

# Credits and Thanksgiving
[@Deep-Symmetry Team](https://github.com/Deep-Symmetry) to create the documentation about how the protocol works.</br>
Amccanmir: A closest friend which helps figure out how the Pioneer device works and give me moral support XD.</br>
[@Plastic Shop](https://plastic.es/) which rents me these expensive devices to do tests and develop the software.</br>
