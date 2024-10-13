# WebRequestHandler

**WebRequestHandler** is a simple utility class designed for Unity projects. It allows you to easily fetch images and videos from the web using asynchronous operations. The class leverages **Cysharp.Threading.Tasks (UniTask)** for smooth and optimized `async/await` handling, ensuring seamless performance in your Unity applications.

## Features

- **Fetch Textures**: Download and display images from a given URI as `Texture` objects.
- **Fetch and Play Videos**: Download and play videos from a given URI using Unity's `VideoPlayer` component.
- **Cancellation Support**: Includes support for cancellation tokens to gracefully handle object destruction or task interruption.

## Installation
- Clone or download the repository.
- Import the necessary Cysharp.Threading.Tasks library into your Unity project.
- Add the WebRequestHandler.cs file to your Unity project.

### Prerequisites
- **Cysharp.Threading.Tasks (UniTask)** installed via https://github.com/Cysharp/UniTask
