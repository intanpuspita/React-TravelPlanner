import React from 'react';
import { render } from 'react-dom';
import './style/index.css';
import './fonts/font-geomanist/geomanist.css';

import { Login } from './components/login'

//render(<Login />, document.getElementById('react-container'));

// import reducer, store, and initial state
import storeFactory from './store';
import { Provider } from 'react-redux';
import { BrowserRouter, Switch, Route, NotFoundRoute } from 'react-router-dom'

import LoginContainer from './containers/login';

const store = storeFactory();
window.store = store;

render(
    <Provider store={store}>
        {/*<BrowserRouter>
            <div>
                <Switch>
                    <Route exact path='/' component={ Login }/>
                    <Route path="/store/:storeId" component={ StoreContainer }/>
                    <Route path="*" component={ Whoops404 }/>
                    {<NotFoundRoute handler={Whoops404} />}
                </Switch>
            </div>
        </BrowserRouter>*/}
        <LoginContainer />
    </Provider>
    , document.getElementById('react-container'));