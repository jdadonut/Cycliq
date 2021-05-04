all : clean restore build publish

clean:
	dotnet clean

restore:
	dotnet restore

build: 
	dotnet build

publish:
	echo "do later"
run:
	dotnet run
heroku:
	dotnet publish ./Cycliq.csproj -r linux-x64 -o ./Run/ --nologo -v n