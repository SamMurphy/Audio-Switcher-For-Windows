# Windows Audio Switch
Command line tool to quickly switch between audio output devices on windows.

When launched will set the next audio device to default.

Loads the names of the audio devices through a text file that should be passed as an argument.

The names of the devices should be the primary name listed in the playback tab of the Windows Sound window.

Example audioDevices.txt:

    Headphones
    Speakers
    DELL U2715H
    FiiO DAC

This project uses the SetDefualtAudioEndpoint library

![Alt text](/icon.ico)
