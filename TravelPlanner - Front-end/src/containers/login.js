import { Login } from '../components/login';
import { connect } from 'react-redux';
import { showAlertLogin } from '../actions';

const mapStateToProps = (state) => {
    return {
        alertLogin: state.alertLogin
    }
}

const mapDispatchToProps = (dispatch) => {
    return {
        onShowAlert: (show) => {
            dispatch(showAlertLogin(show))
        }
    }
}

export default connect(mapStateToProps, mapDispatchToProps)(Login);