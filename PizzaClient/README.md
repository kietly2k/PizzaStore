## Connect to a back-end API
- You have a front-end app. What do you need to think about for the back-end application? Well, you're either:
 - **Working against mocked data**. During the development phase, you can work independently, as a standalone team. But you still want to build out the application and at least emulate that you're working with an API. The `json-server` library creates a RESTful API for you, from a static JSON file
 - **Talking to a real API**. If you're in this phase, the back-end team has built the API, and now you want to connect it to your front-end.

> Note: You may notice that this JSON uses double quotes for the property names. In JavaScript, you can use single quotes, double quotes, or no quotes for property names, but when using JSON outside of JavaScript, the syntax must be correct with double quotes.

## Talk to API with proxy
A proxy is a server that sits between the front-end app and the back-end API. The front-end app makes requests toward the proxy, and the proxy forwards the request toward the back-end API. The proxy can also forward the response back to the front-end app. Use a proxy to make requests toward the mocked API.

## Design the user interface (UI)
[Design systems for front-end apps](https://learn.microsoft.com/en-us/training/modules/build-web-api-minimal-spa/6-design-style)

## React
### 1.One-way data binding
The ability to pass information from a parent component to a child component is referred to as one-way data binding. The data always moves from the parent to the child. The child component can't change the data. If the child component needs to change the data, it must send a message to the parent component, and the parent component can then change the data. This allows the parent to control when the child is rerendered. Over-rendering in React can be a performance issue, so it's important to be aware of this.

### 2.Data binding and state management
In order to have efficient component rendering, you need to plan state management. State management is the process of managing the data that's used by your components. There are state management systems such as Redux