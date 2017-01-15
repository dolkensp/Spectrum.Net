# Spectrum.Net

This project is a basic API to interact with the Spectrum platform for Star Citizen.

You will either need to uncomment the username/password login method, and pass your credentials, or you will need to add a `private-appSettings.config` file, with a `Token.User` key set to match your Spectrum `x-rsi-ptu-token`

Currently implemented methods allow basic subscribe/read/write/erase/history functionality, but this is a super-early alpha at this stage, as a sneak peak at what's coming.
