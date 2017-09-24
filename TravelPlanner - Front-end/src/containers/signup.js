import { Signup } from '../components/signup';
import { connect } from 'react-redux';
import { showAlertLogin } from '../actions';

const mapStateToProps = (state) => {
    return {
        alertLogin: state.alertLogin
    }
}

const mapDispatchToProps = (dispatch) => {
    return {
        onShowAlert: (e, show) => {
            e.preventDefault();
            dispatch(showAlertLogin(show))
        }
    }
}

export default connect(mapStateToProps, mapDispatchToProps)(Signup);