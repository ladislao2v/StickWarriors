using System.Collections.Generic;
using System.Linq;
using Plugins.DynamicBehaviour.Interfaces;
using UnityEngine;

namespace Plugins.DynamicBehaviour
{
    public class DynamicObject : MonoBehaviour
    {
        [Header("Object mechanics"), Tooltip("List of mechanics which you can add to the object")]
        [SerializeReference] private List<IMechanics> _mechanics = new List<IMechanics>();
        [Header("Object views"), Tooltip("List of views that represent the object")]
        [SerializeReference] private List<IView> _views = new List<IView>(); 

        private IReadOnlyList<IUpdatable> Updatables =>
            _mechanics.OfType<IUpdatable>()
                .Concat(_views.OfType<IUpdatable>()).ToList();

        private IReadOnlyList<ILateUpdatable> LateUpdatables =>
            
            _mechanics.OfType<ILateUpdatable>()
                .Concat(_views.OfType<ILateUpdatable>()).ToList();

        private IReadOnlyList<IFixedUpdatable> FixedUpdatables =>
            _mechanics.OfType<IFixedUpdatable>()
                .Concat(_views.OfType<IFixedUpdatable>()).ToList();

        private void Awake()
        {
            foreach (var initializable in _mechanics.OfType<IInitializable>())
                initializable.Construct(this);

            foreach (var initializable in _views.OfType<IInitializable>())
                initializable.Construct(this);
        }

        private void Update()
        {
            foreach (var updatable in Updatables)
                updatable.Update();
        }

        private void LateUpdate()
        {
            foreach(var lateUpdatable in LateUpdatables)
                lateUpdatable.LateUpdate();
        }

        private void FixedUpdate()
        {
            foreach (var fixedUpdatable in FixedUpdatables)
                fixedUpdatable.FixedUpdate();    
        }

        private void OnEnable()
        {
            foreach (var mechanics in _mechanics)
                mechanics.Enable();
            
            foreach (var view in _views)
                view.Enable();
        }

        private void OnDisable()
        {
            foreach (var mechanics in _mechanics)
                mechanics.Disable();
            
            foreach (var view in _views)
                view.Disable();
        }

        public void AddMechanics(IMechanics mechanics)
        {
            if (_mechanics.Contains(mechanics))
                return;
            
            if(mechanics is IInitializable initializable)
                initializable.Construct(this);

            _mechanics.Add(mechanics);
        }

        public void AddView(IView view)
        {
            if (_views.Contains(view))
                return;
            
            if(view is IInitializable initializable)
                initializable.Construct(this);

            _views.Add(view);
        }

        public bool TryRemoveMechanics<TMechanics>() where TMechanics : class, IMechanics
        {
            var mechanics = (IMechanics) FindObjectPart<TMechanics>(_views.Cast<ObjectPart>().ToList());

            if (mechanics == null)
                return false;
            
            _mechanics.Remove(mechanics);
            return true;
        }

        public bool TryRemoveView<TView>() where TView : class, IView
        {
            var view = (IView) FindObjectPart<TView>(_views.Cast<ObjectPart>().ToList());

            if (view == null)
                return false;
            
            _views.Remove(view);
            return true;
        }

        public bool TryGetMechanics<TMechanics>(out TMechanics mechanics)
        {
            mechanics = (TMechanics) _mechanics.FirstOrDefault<IMechanics>(objectMechanics => objectMechanics is TMechanics);

            return mechanics != null;
        }

        public bool TryGetView<TView>(out TView view)
        {
            view = (TView) _views.FirstOrDefault<IView>(objectView => objectView is TView);

            return view != null;
        }

        private ObjectPart FindObjectPart<TPart>(List<ObjectPart> objectParts) where TPart : class
        {
            var objectPart = objectParts.FirstOrDefault(
                mechanics =>
                    mechanics is TPart);
            
            return objectPart;
        }
    }
}