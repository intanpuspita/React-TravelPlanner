import React from 'react';
import { Col, Alert } from 'react-bootstrap';

import SignupContainer from '../containers/signup';

const SuccessRegister = (props) => {
    if(props.show){
        return(
            <Alert bsStyle="success">
                <strong>Thank you for register!</strong> Just one step closer. Activate your account by clicking the link in your email
            </Alert>
        );
    }
    
    return null;
}

const LogoAndTagline = () => {
    return(
        <div className="header-text">
            <span>It's time to </span>
            <div className="slidingVertical">
                <span>discover.</span>
                <span>play.</span>
                <span>have fun.</span>
                <span>run away.</span>
                <span>being lost.</span>
            </div>
        </div>
    );
}

export class Login extends React.Component {
    render(){
        return(
            <div className="login-page login-image">

                <Col className="login-full-col" xs={12} md={12}>
                    {/*<div className="signin-container">
                        <form id="signin">
                            <input type="text" placeholder="Email" />
                        </form>
                    </div>*/}
                    <SuccessRegister show={this.props.alertLogin}/>
                </Col>

                <Col className="login-left-col" xs={12} md={6}>
                    <LogoAndTagline />
                </Col>

                <Col className="login-right-col" xs={12} md={6}>
                    <SignupContainer />
                </Col>
                
            </div>
        )
    }
}