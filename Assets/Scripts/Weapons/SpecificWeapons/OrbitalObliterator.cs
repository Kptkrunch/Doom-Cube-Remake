using UnityEngine;

namespace Weapons.SpecificWeapons
{
    public class OrbitalObliterator : Weapon
    {
        public LineRenderer theBeam;
        public GameObject beamFrame, beamOrigin; 
        public GameObject beamSignal, beamImpact;
    
        private float _beamCooldown, _beamDuration, _beamCdTimer, _beamDurationTimer, _moveSpeed, _signalRange;
        private bool _isFiring, _gotLocation;
        private Vector3 _newSignalPosition, _beamStrikePosition;
        private void Start()
        {
            var position = beamSignal.transform.position;
            beamOrigin.transform.position = new Vector2(position.x, position.y + 10);
            SetStats();
        }

        private void FixedUpdate()
        {
            if (_gotLocation)
            {
                _gotLocation = false;
                GetNewOrbitalSignal();
            }

            if (_isFiring)
            {
                beamFrame.transform.position = Vector3.MoveTowards(beamFrame.transform.position, _newSignalPosition, _moveSpeed * Time.deltaTime);
            }
     
            beamFrame.SetActive(_isFiring);
            Laser();
            BeamTimeHandler();
        }

        private void GetNewOrbitalSignal()
        {
            var x = Random.Range(-_signalRange, _signalRange);
            var y = Random.Range(-_signalRange, _signalRange);
            _newSignalPosition = new Vector2(x, y);
            beamSignal.transform.position = _newSignalPosition;
        }

        private void SetStats()
        {
            _beamCooldown = stats.weaponLvls[stats.lvl].coolDown;
            _beamDuration = stats.weaponLvls[stats.lvl].duration;
            _moveSpeed = stats.weaponLvls[stats.lvl].speed;
            _signalRange = stats.weaponLvls[stats.lvl].range * 4f;
            gameObject.transform.localScale = new Vector3(1f, 1f, 1f);
            beamImpact.GetComponent<CircleCollider2D>().radius = stats.weaponLvls[stats.lvl].range;
        }

        private void BeamTimeHandler()
        {
            if (_isFiring) {
                _beamCdTimer = _beamCooldown;
                _beamDurationTimer -= Time.deltaTime;
                if (_beamDurationTimer <= 0)
                {
                    _isFiring = false;
                }
            } else if (!_isFiring)
            {
                _beamDurationTimer = _beamDuration;
                _beamCdTimer -= Time.deltaTime;
                if (_beamCdTimer <= 0)
                {
                    _isFiring = true;
                    _gotLocation = true;
                }
            }
        }

        private void Laser()
        {
            if (_isFiring)
            {
                _beamStrikePosition = beamImpact.transform.position;
                theBeam.SetPosition(0, beamOrigin.transform.position);
                theBeam.SetPosition(1, _beamStrikePosition);
            }
        }
    }
}
