import React from 'react';
import axios from 'axios';

export class Signup extends React.Component {
    constructor(props) {
        super(props);
    }

    register(e, onSuccess){
        e.preventDefault();

        /*axios({
            method: 'post',
            url: 'http://localhost:5001/api/Account',
            data: JSON.stringify({
                "email": this.email.value,
                "name": this.name.value,
                "password": this.password.value
            }),
            headers: {
                "Accept": "application/json",
                "Content-Type": "application/json;charset=UTF-8"
            }
        })
        .then(function (response) {*/
            onSuccess(e, true);
        /*})
        .catch(function (error) {
            console.log(error);
        });*/
    }

    render(){
        return(
            <div className="signup-container">
                <div className="signup-div">
                    <h3>Create new account</h3>
                    <p>Register your account and start planning your next adventure</p>
                    
                    <form ref={(input) => this.registerForm = input} id="register" onSubmit={(e) => this.register(e, this.props.onShowAlert)} className="full-width-input">
                        <input ref={(input) => this.name = input} type="text" placeholder="Enter your name" required />
                        <input ref={(input) => this.email = input} type="email" placeholder="Enter your email" required />
                        <input ref={(input) => this.password = input} type="password" placeholder="Enter your password" required />
                        <button type="submit">Register</button>
                    </form>

                    <div className="or-row">
                        <div className="or-line"></div>
                        <div className="or-text">or</div>
                        <div className="or-line"></div>
                    </div>

                    <div id="btn-google" className="g-signin2" data-onsuccess="onSignIn" data-theme="light"></div>
                </div>
            </div>
        )
    }
}