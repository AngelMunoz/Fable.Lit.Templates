module Components.Navbar

open Browser.Types
open Fable.Core
open Lit
open Fable.Haunted
open Types


let View (onBackRequested: _ -> unit) (goToPage: Page -> _ -> unit) =
    html
        $"""
        <nav role="navigation">
            <button @click={onBackRequested}>Back</button>
            <button @click={goToPage Page.Home}>Home</button>
            <button @click={goToPage Page.Notes}>Notes</button>
        </nav>
        """
