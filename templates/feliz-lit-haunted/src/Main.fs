module Main

open Pages

Fable.Core.JsInterop.importSideEffects "./styles.css"

// register your custom elements here
Home.register ()
Notes.register ()
App.register ()
