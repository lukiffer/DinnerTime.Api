# Register the trusted Microsoft signature key
curl https://packages.microsoft.com/keys/microsoft.asc | gpg --dearmor > microsoft.gpg
sudo mv microsoft.gpg /etc/apt/trusted.gpg.d/microsoft.gpg

# Register the Microsoft Product feed
sudo sh -c 'echo "deb [arch=amd64] https://packages.microsoft.com/repos/microsoft-ubuntu-xenial-prod xenial main" > /etc/apt/sources.list.d/dotnetdev.list'

# Install .NET Core SDK
sudo apt-get update
sudo apt-get install dotnet-sdk-2.0.0

# Copy the systemd service file
sudo cp dinnertime.service /lib/systemd/system/

# Reload systemd
sudo systemctl daemon-reload

# Enable the service
sudo systemctl enable dinnertime

# build and start the service
sh build.sh
