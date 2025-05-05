'use strict';
import {CModal} from './lib/CModal.js';


//-------------------------------------------------------------------------------
//--------------------------------CMessageModal----------------------------------
//-------------------------------------------------------------------------------
export class CMessageModal extends CModal {
    #messageElem;
    constructor(modalCanvasSelector, outerCanvasClickClosesPopup = false) {
        super(modalCanvasSelector, outerCanvasClickClosesPopup);
        this.#messageElem = document.querySelector(`${modalCanvasSelector} .message-display`);
    }

    display(message, isHtml = true, timeout = 0) {
        if (isHtml) {
            this.#messageElem.innerHTML = message;
        } else {
            this.#messageElem.innerText = message;
        }

        super.show(timeout);
    }

    timerCompleted() {
        this.#messageElem.innerText = "";
        super.timerCompleted();
    }
}


//-------------------------------------------------------------------------------
//--------------------------------CPlayerModal-----------------------------------
//-------------------------------------------------------------------------------
export class CPlayerModal extends CModal {
    playerUserName = '';
    playerPassword = '';
    #form = this.mainPanel.querySelector('form');
   
    constructor(modalCanvasSelector, outerCanvasClickClosesPopup = false, startupCallbackFunction = ()=>{}, isCallBackNeeded = false) {
        super(modalCanvasSelector, outerCanvasClickClosesPopup);
        this.startupCallbackFunction = startupCallbackFunction;
    }

    get playerFullName() {
        return `${this.playerFirstName} ${this.playerLastName}`;
    }
    set startupCallbackFunction(callbackFunction) {
        this.#form.btnOpenSignUp.addEventListener('click', function (e) {
            document.getElementById('modal-signup').classList.remove('hidden')
            document.getElementById('modal-player-id').classList.add('hidden')
            e.preventDefault();
            console.log('opened sign up form')
        });
        document.getElementById('btnBack').addEventListener('click', function (e) {
            document.getElementById('modal-signup').classList.add('hidden');
            document.getElementById('modal-player-id').classList.remove('hidden');
            e.preventDefault();
            console.log('went back to login form');
        });
       
        this.#form.btnBeginGame.addEventListener('click', event=> {
            //console.log(`event.cancelable: ${event.cancelable}`);
            if (isCallBackNeeded) {
                event.preventDefault();  
            }
            
            // As the button type is submit, this prevents postback to the server
            event.stopPropagation();    // prevent event bubbling up to parent(s)

            this.#form.txtUsername.value = this.#form.txtUsername.value.trim();
            this.#form.txtPassword.value = this.#form.txtPassword.value.trim();

            if (!this.#form.txtUsername.value) {
                this.#form.txtUsername.focus();
                return;
            }

            if (!this.#form.txtPassword.value) {
                this.#form.txtPassword.focus();
                return;
            }

            this.playerUserName = this.#form.txtUsername.value;
            this.playerPassword = this.#form.txtPassword.value;
            

            callbackFunction();     // invoke the callback function
        });
    }

    display(timeout = 0) {
        this.#form.txtUsername.value = this.playerUserName;
        this.#form.txtPassword.value = this.playerPassword;
        super.show(timeout);
        this.#form.txtUsername.select();
        this.#form.txtPassword.focus();
    }
}


//-------------------------------------------------------------------------------
//--------------------------------CWinnerModal-----------------------------------
//-------------------------------------------------------------------------------
export class CWinnerModal extends CModal {
    display(elapsed, hits, misses, miscMessage = '', timeout = 0) {

        const form = this.mainPanel.querySelector('form');
        form.numElapsedTime.value = elapsed;
        form.numHits.value = hits;
        form.numMisses.value = misses;
        form.txtMiscMessage.value = miscMessage;

        super.show(timeout);
    }
}