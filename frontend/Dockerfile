# Create image based on the official Node 14 image from dockerhub
FROM node:14

# Change directory so that our commands run inside this new directory
WORKDIR /frontend

# Copy dependency definitions
COPY ["frontend/package*.json", "./"]

# Install dependecies
RUN npm install && npm install -g @angular/cli
RUN ls
# Get all the code needed to run the app
COPY ["frontend/", "./"]

# Expose the port the app runs in
EXPOSE 4200

# Serve the app
CMD ng serve --host 0.0.0.0
